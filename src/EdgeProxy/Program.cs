using System.Text.RegularExpressions;
using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

var bucketConnectionString = builder.Configuration.GetConnectionString("bucket");

if (string.IsNullOrEmpty(bucketConnectionString))
{
	throw new InvalidOperationException("The connection string for the bucket is not set.");
}

var primaryMatch = Regex.Match(bucketConnectionString, "Primary=([^;]+)", RegexOptions.IgnoreCase);

if (!primaryMatch.Success)
{
	throw new InvalidOperationException("The connection string for the bucket is invalid.");
}

var bucketUrl = primaryMatch.Groups[1].Value;

var bucketNameMatch = Regex.Match(bucketConnectionString, "BucketName=([^;]+)", RegexOptions.IgnoreCase);

if (!bucketNameMatch.Success)
{
	throw new InvalidOperationException("The connection string for the bucket is invalid.");
}

var bucketName = bucketNameMatch.Groups[1].Value;

List<RouteConfig> routes = [
	new RouteConfig {
		RouteId = "bucket",
		ClusterId = "bucket",
		Match = new RouteMatch
		{
			Path = "/{**catch-all}"
		}
	},
];

List<ClusterConfig> clusters = [
	new ClusterConfig {
		ClusterId = "bucket",
		Destinations = new Dictionary<string, DestinationConfig>() {
			["bucket"] = new DestinationConfig
			{
				Address = bucketUrl,
			}
		}
	},
];

builder.Services.AddReverseProxy()
	.LoadFromMemory(routes, clusters)
	.AddTransforms(builderContext =>
	{
		builderContext.AddPathPrefix($"/{bucketName}");
	});

var app = builder.Build();

app.MapReverseProxy();

app.MapGet("/_health", () => "Ok!");

app.Run();
