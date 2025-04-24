namespace HexaContent.EdgeProxy;

public class RedirectionsMiddleware(RequestDelegate next)
{
	public async Task InvokeAsync(HttpContext context)
	{
		if (context.Request.Method != "GET")
		{
			await next(context);
			return;
		}

		var path = context.Request.Path.Value;

		if (path == "/test-redirection")
		{
			string destination = "/test-redirection-2";
			context.Response.StatusCode = 301;
			context.Response.Headers.Location = destination;
			await context.Response.WriteAsync($"Redirecting to {destination}");
			return;
		}

		await next(context);
		return;
	}
}
