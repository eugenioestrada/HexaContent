using Yarp.ReverseProxy.Configuration;
using Yarp.ReverseProxy.Transforms;

namespace HexaContent.EdgeProxy.Config;

public static class ConfigExtensions
{
	private const string ClusterId = "bucket";
	private const string RootRouteId = "root";
	private const string RootPath = "/";
	private const string AllRouteId = "all";
	private const string HeadMethod = "HEAD";
	private const string GetMethod = "GET";
	private static readonly List<string> headersToRemove = [ "x-amz-request-id", "x-amz-id-2", "x-ratelimit-limit", "x-ratelimit-remaining", "server", "last-modified", "date" ];

	public static void HandleHead(this WebApplication app)
	{
		app.MapWhen(ctx => ctx.Request.Method == HeadMethod && ctx.Request.Path == RootPath, HeadMiddleware);
	}

	public static void ConfigureReverseProxy(this WebApplicationBuilder builder, string bucketUrl, string bucketName)
	{
		List<RouteConfig> routes = [
			new RouteConfig {
				RouteId = RootRouteId,
				ClusterId = ClusterId,
				Match = new RouteMatch
				{
					Path = RootPath
				}
			},
			new RouteConfig {
				RouteId = "with-ext-0",
				ClusterId = ClusterId,
				Match = new RouteMatch
				{
					Path = "/{filename}.{ext}",
					Methods = [ GetMethod ]
				}
			},
			new RouteConfig {
				RouteId = "with-ext-1",
				ClusterId = ClusterId,
				Match = new RouteMatch
				{
					Path = "/{path1}/{filename}.{ext}",
					Methods = [ GetMethod ]
				}
			},
			new RouteConfig {
				RouteId = "with-ext-2",
				ClusterId = ClusterId,
				Match = new RouteMatch
				{
					Path = "/{path1}/{path2}/{filename}.{ext}",
					Methods = [ GetMethod ]
				}
			},
			new RouteConfig {
				RouteId = "with-ext-3",
				ClusterId = ClusterId,
				Match = new RouteMatch
				{
					Path = "/{path1}/{path2}/{path3}/{filename}.{ext}",
					Methods = [ GetMethod ]
				}
			},
			new RouteConfig {
				RouteId = "with-ext-4",
				ClusterId = ClusterId,
				Match = new RouteMatch
				{
					Path = "/{path1}/{path2}/{path3}/{path4}/{filename}.{ext}",
					Methods = [ GetMethod ]
				}
			},
			new RouteConfig {
				RouteId = AllRouteId,
				ClusterId = ClusterId,
				Match = new RouteMatch
				{
					Path = "/{**catch-all}",
					Methods = [ GetMethod ]
				}
			}
		];

		List<ClusterConfig> clusters = [
			new ClusterConfig {
				ClusterId = ClusterId,
				Destinations = new Dictionary<string, DestinationConfig>() {
					[ "bucket" ] = new DestinationConfig
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
				if (builderContext.Route.RouteId != RootRouteId)
				{
					builderContext.AddPathPrefix($"/{bucketName}");
				}
				else
				{
					builderContext.AddPathSet($"/{bucketName}/index.html");
				}

				foreach (var header in headersToRemove)
				{
					builderContext.AddResponseHeaderRemove(header);
				}

				builderContext.AddResponseHeader("server", "Difoosion Proxy");

				if (builderContext.Route.RouteId == AllRouteId)
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
