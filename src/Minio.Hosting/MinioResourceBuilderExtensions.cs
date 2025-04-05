using Amazon.S3;
using Amazon.S3.Model;
using Aspire.Hosting.ApplicationModel;
using System.Net;
using System.Text.RegularExpressions;

namespace Aspire.Hosting;

/// <summary>
/// Provides extension methods for building and configuring Minio resources.
/// </summary>
public static partial class MinioResourceBuilderExtensions
{
	private const string AddressEnvVarName = "MINIO_ADDRESS";
	private const string ConsoleAddressEnvVarName = "MINIO_CONSOLE_ADDRESS";
	private const string UserEnvVarName = "MINIO_ROOT_USER";
	private const string PasswordEnvVarName = "MINIO_ROOT_PASSWORD";

	private const int MinioPort = 9000;
	private const int MinioConsolePort = 9001;
	public const string MinioUser = "minio_user";
	public const string MinioPassword = "minio_password";

	/// <summary>
	/// Gets the regular expression used to parse the connection string.
	/// </summary>
	/// <returns>The regular expression for parsing the connection string.</returns>
	[GeneratedRegex("Primary=([^;]+);Console=([^;]+)", RegexOptions.IgnoreCase, "en-US")]
	private static partial Regex ConnectionStringRegex();

	/// <summary>
	/// Adds a Minio resource to the distributed application builder.
	/// </summary>
	/// <param name="builder">The distributed application builder.</param>
	/// <param name="name">The name of the Minio resource.</param>
	/// <param name="httpPort">The optional HTTP port for the Minio resource.</param>
	/// <returns>The resource builder for the Minio resource.</returns>
	public static IResourceBuilder<MinioResource> AddMinio(
		this IDistributedApplicationBuilder builder,
		string name,
		int? httpPort = null)
	{
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
				var match = ConnectionStringRegex().Match(connectionString);
				AmazonS3Client client = new(MinioUser, MinioPassword, new AmazonS3Config
				{
					ServiceURL = match.Groups[1].Value,
					ForcePathStyle = true,
				});

				var buckets = await client.ListBucketsAsync();
				foreach (var bucket in resource.Buckets)
				{
					if (!buckets.Buckets.Any(b => b.BucketName == bucket.Value))
					{
						var response = await client.PutBucketAsync(new PutBucketRequest
						{
							BucketName = bucket.Value,
							CannedACL = S3CannedACL.PublicRead,
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

	/// <summary>
	/// Adds a bucket to the Minio resource.
	/// </summary>
	/// <param name="builder">The resource builder for the Minio resource.</param>
	/// <param name="name">The name of the bucket resource.</param>
	/// <param name="bucketName">The optional bucket name. If not provided, the name parameter will be used.</param>
	/// <returns>The resource builder for the Minio bucket resource.</returns>
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
