using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting;

namespace HexaContent.Minio.Hosting;

public static class MinioResourceBuilderExtensions
{
	private const string AddressEnvVarName = "MINIO_ADDRESS";
	private const string ConsoleAddressEnvVarName = "MINIO_CONSOLE_ADDRESS";
	private const string UserEnvVarName = "MINIO_ROOT_USER";
	private const string PasswordEnvVarName = "MINIO_ROOT_PASSWORD";

	private const int MinioPort = 9000;
	private const int MinioConsolePort = 9001;
	private const string MinioUser = "minio_user";
	private const string MinioPassword = "minio_password";

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
}
