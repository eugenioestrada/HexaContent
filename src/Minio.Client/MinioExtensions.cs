using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HexaContent.Minio.Client;

public static class MinioExtensions
{
	private const string DefaultConfigSectionName = "Aspire:Minio:Client";

	public static void AddMinioClient(
		this IHostApplicationBuilder builder,
		string connectionName,
		Action<MinioClientSettings>? configureSettings = null) =>
		AddMinioClient(
			builder,
			DefaultConfigSectionName,
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

		if (builder.Configuration.GetConnectionString(connectionName) is string connectionString)
		{
			MinioClientSettings settings = new MinioClientSettings(connectionString);

			if (serviceKey is null)
			{
				builder.Services.AddSingleton(CreateMinioClientFactory);
			}
			else
			{
				builder.Services.AddKeyedSingleton(serviceKey, (sp, key) => CreateMinioClientFactory(sp));
			}

			MinioFactory CreateMinioClientFactory(IServiceProvider _)
			{
				return new MinioFactory(settings);
			}
		}
	}
}
