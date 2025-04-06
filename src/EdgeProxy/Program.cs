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
		RouteId = "root",
		ClusterId = "bucket",
		Match = new RouteMatch
		{
			Path = "/"
		}
	},
	new RouteConfig {
		RouteId = "with-ext-0",
		ClusterId = "bucket",
		Match = new RouteMatch
		{
			Path = "/{filename}.{ext}",
			Methods = [ "GET" ]
		}
	},
	new RouteConfig {
		RouteId = "with-ext-1",
		ClusterId = "bucket",
		Match = new RouteMatch
		{
			Path = "/{path1}/{filename}.{ext}",
			Methods = [ "GET" ]
		}
	},
	new RouteConfig {
		RouteId = "with-ext-2",
		ClusterId = "bucket",
		Match = new RouteMatch
		{
			Path = "/{path1}/{path2}/{filename}.{ext}",
			Methods = [ "GET" ]
		}
	},
	new RouteConfig {
		RouteId = "with-ext-3",
		ClusterId = "bucket",
		Match = new RouteMatch
		{
			Path = "/{path1}/{path2}/{path3}/{filename}.{ext}",
			Methods = [ "GET" ]
		}
	},
	new RouteConfig {
		RouteId = "with-ext-4",
		ClusterId = "bucket",
		Match = new RouteMatch
		{
			Path = "/{path1}/{path2}/{path3}/{path4}/{filename}.{ext}",
			Methods = [ "GET" ]
		}
	},
	new RouteConfig {
		RouteId = "all",
		ClusterId = "bucket",
		Match = new RouteMatch
		{
			Path = "/{**catch-all}",
			Methods = [ "GET" ]
		}
	}
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
		if (builderContext.Route.RouteId != "root")
		{
			builderContext.AddPathPrefix($"/{bucketName}");
		}
		else
		{
			builderContext.AddPathSet($"/{bucketName}/test.html");
		}

		if (builderContext.Route.RouteId == "all")
		{
			builderContext.AddRequestTransform(async (transformContext) =>
			{
				transformContext.Path = new PathString($"{transformContext.Path}.html");
			});
		}
	});

builder.Services.AddOutputCache(options =>
{
	options.AddPolicy("customPolicy", builder => builder.Expire(TimeSpan.FromSeconds(20)));
});

var app = builder.Build();

app.UseOutputCache();

app.MapReverseProxy();

app.MapGet("/_health", () => "Ok!");

app.Run();
