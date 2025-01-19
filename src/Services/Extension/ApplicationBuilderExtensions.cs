using HexaContent.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HexaContent.Services.Extension;

/// <summary>
/// Provides extension methods for configuring the application builder.
/// </summary>
public static class ApplicationBuilderExtensions
{
	/// <summary>
	/// Adds repositories to the application builder.
	/// </summary>
	/// <param name="builder">The application builder.</param>
	public static void AddServices(this IHostApplicationBuilder builder)
    {
		builder.Services.AddScoped<IContentService, ContentService>();
	}
}
