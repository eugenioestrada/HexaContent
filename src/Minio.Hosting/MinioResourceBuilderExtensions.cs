using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Amazon.S3;
using System.Runtime.CompilerServices;
using Amazon.S3.Model;
using System.Net;

namespace Aspire.Hosting;

public static class MinioResourceBuilderExtensions
{
	private const string AddressEnvVarName = "MINIO_ADDRESS";
	private const string ConsoleAddressEnvVarName = "MINIO_CONSOLE_ADDRESS";
	private const string UserEnvVarName = "MINIO_ROOT_USER";
	private const string PasswordEnvVarName = "MINIO_ROOT_PASSWORD";

	private const int MinioPort = 9000;
	private const int MinioConsolePort = 9001;
	public const string MinioUser = "minio_user";
	public const string MinioPassword = "minio_password";

	public static IResourceBuilder<MinioResource> AddMinio(
		this IDistributedApplicationBuilder builder,
		string name,
		int? httpPort = null)
	{

		// The AddResource method is a core API within .NET Aspire and is
		// used by resource developers to wrap a custom resource in an
		// IResourceBuilder<T> instance. Extension methods to customize
		// the resource (if any exist) target the builder interface.
		var resource = new MinioResource(name);

		string? connectionString = null;

		builder.Eventing.Subscribe<ConnectionStringAvailableEvent>(resource, async (@event, ct) =>
		{
			connectionString = await resource.ConnectionStringExpression.GetValueAsync(ct).ConfigureAwait(false);

			if (connectionString == null)
			{
				throw new DistributedApplicationException($"ConnectionStringAvailableEvent was published for the '{resource.Name}' resource but the connection string was null.");
			}
		});

		builder.Eventing.Subscribe<ResourceReadyEvent>(resource, async (@event, ct) =>
		{
			if (resource.Buckets.Any())
			{
				AmazonS3Client client = new(MinioUser, MinioPassword, new AmazonS3Config
				{
					ServiceURL = connectionString,
					ForcePathStyle = true,
				});
				var buckets = await client.ListBucketsAsync();
				foreach (var bucket in resource.Buckets)
				{
					if (!buckets.Buckets.Any(b => b.BucketName == bucket.Value))
					{
						var response = await client.PutBucketAsync(new PutBucketRequest
						{
							BucketName = bucket.Value
						});

						if (response.HttpStatusCode == HttpStatusCode.OK)
						{
							throw new DistributedApplicationException($"Failed to create bucket '{bucket.Value}' in Minio.");
						}
					}
				}
			}
		});

		return builder.AddResource(resource)
					  .WithImage(MinioContainerImageTags.Image)
					  .WithImageRegistry(MinioContainerImageTags.Registry)
					  .WithImageTag(MinioContainerImageTags.Tag)
					  .WithVolume("data", "/data")
					  .WithEnvironment(context =>
					  {
						  context.EnvironmentVariables[AddressEnvVarName] = $":{MinioPort}";
						  context.EnvironmentVariables[ConsoleAddressEnvVarName] = $":{MinioConsolePort}";
						  context.EnvironmentVariables[UserEnvVarName] = MinioUser;
						  context.EnvironmentVariables[PasswordEnvVarName] = MinioPassword;
					  })
					  .WithHttpEndpoint(targetPort: MinioPort, name: MinioResource.PrimaryEndpointName)
					  .WithHttpEndpoint(targetPort: MinioConsolePort, name: MinioResource.ConsoleEndpointName)
					  .WithArgs("server", "/data");
	}

	public static IResourceBuilder<MinioBucketResource> AddBucket(
		this IResourceBuilder<MinioResource> builder,
		[ResourceName] string name, 
		string? bucketName = null)
	{
		bucketName ??= name;

		builder.Resource.AddBucket(name, bucketName);

		var bucketResource = new MinioBucketResource(name, bucketName, builder.Resource);
		return builder.ApplicationBuilder.AddResource(bucketResource);
	}
}
