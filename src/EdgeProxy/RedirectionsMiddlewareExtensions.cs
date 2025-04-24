namespace HexaContent.EdgeProxy;

public static class RedirectionsMiddlewareExtensions
{
	public static IApplicationBuilder UseRedirections(
		this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<RedirectionsMiddleware>();
	}
}
