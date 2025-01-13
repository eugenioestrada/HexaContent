using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HexaContent.Minio.Client;

public static class MinioExtensions
{
	private const string DefaultConfigSectionName = "HexaContent:Minio:Client";

	public static void AddMinioClient(
		this IHostApplicationBuilder builder,
		string connectionName,
		Action<MinioClientSettings>? configureSettings = null) =>
		AddMinioClient(
			builder,
			MinioClientSettings.DefaultConfigSectionName,
			configureSettings,
			connectionName,
			serviceKey: null);

	private static void AddMinioClient(
		this IHostApplicationBuilder builder,
		string configurationSectionName,
		Action<MinioClientSettings>? configureSettings,
		string connectionName,
		object? serviceKey)
	{
		ArgumentNullException.ThrowIfNull(builder);

		var settings = new MinioClientSettings();

		builder.Configuration
			   .GetSection(configurationSectionName)
			   .Bind(settings);

		configureSettings?.Invoke(settings);

		if (serviceKey is null)
		{
			builder.Services.AddScoped(CreateMinioClientFactory);
		}
		else
		{
			builder.Services.AddKeyedScoped(serviceKey, (sp, key) => CreateMinioClientFactory(sp));
		}

		MinioFactory CreateMinioClientFactory(IServiceProvider _)
		{
			return new MinioFactory(settings);
		}
	}
}
