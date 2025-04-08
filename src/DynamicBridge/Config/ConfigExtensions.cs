namespace HexaContent.DynamicBridge.Config;

public static class ConfigExtensions
{
	public static void HandleHead(this WebApplication app)
	{
		app.MapWhen(ctx => ctx.Request.Method == "HEAD" && ctx.Request.Path == "/", HeadMiddleware);
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
