var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

#if DEBUG
if (System.Diagnostics.Debugger.IsAttached)
{
	app.Use(async (context, next) =>
	{
		app.Logger.LogInformation($"REQUEST :: {context.Request.Method} :: {context.Request.Path}");
		await next();
	});
}
#endif

app.MapGet("/", () => "Hello World!");

app.MapGet("/dynamic/", () => "Hello Dynamic!");

app.MapGet("/_health", () => "Ok!");

app.MapWhen(ctx => ctx.Request.Method == "HEAD" && ctx.Request.Path == "/", HandleHead);

app.Run();

static void HandleHead(IApplicationBuilder app)
{
	app.Run(async context =>
	{
		context.Response.StatusCode = 200;
		await context.Response.WriteAsync($"Ok!");
	});
}