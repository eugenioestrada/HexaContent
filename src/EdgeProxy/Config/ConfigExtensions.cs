using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace HexaContent.EdgeProxy.Config;

public static class ConfigExtensions
{
	public static void HandleHead(this WebApplication app)
	{
		app.MapWhen(ctx => ctx.Request.Method == "HEAD" && ctx.Request.Path == "/", HeadMiddleware);
	}

	public static void ConfigureReverseProxy(this WebApplicationBuilder builder, string bucketUrl, string bucketName)
	{
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
					builderContext.AddPathSet($"/{bucketName}/index.html");
				}

				List<string> headersToRemove = [ "x-amz-request-id", "x-amz-id-2", "x-ratelimit-limit", "x-ratelimit-remaining", "server", "last-modified", "date" ];

				foreach (var header in headersToRemove)
				{
					builderContext.AddResponseHeaderRemove(header);
				}

				builderContext.AddResponseHeader("server", "Difoosion Proxy");

				if (builderContext.Route.RouteId == "all")
				{
					builderContext.AddRequestTransform(async (transformContext) =>
					{
						transformContext.Path = new PathString($"{transformContext.Path}.html");
					});
				}
			});
	}

	static void HeadMiddleware(IApplicationBuilder app)
	{
		app.Run(async context =>
		{
			context.Response.StatusCode = 200;
			await context.Response.WriteAsync($"Ok!");
		});
	}
}
