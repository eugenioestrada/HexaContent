using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public static partial class VarnishResourceBuilderExtensions
{
	/// <summary>
	/// Adds a Varnish resource to the distributed application builder.
	/// </summary>
	/// <param name="builder">The distributed application builder.</param>
	/// <param name="name">The name of the Varnish resource.</param>
	/// <param name="httpPort">The optional HTTP port for the Minio resource.</param>
	/// <returns>The resource builder for the Varnish resource.</returns>
	public static IResourceBuilder<VarnishResource> AddVarnish(
		this IDistributedApplicationBuilder builder,
		string name,
		int? httpPort = null)
	{
		var resource = new VarnishResource(name);

		return builder.AddResource(resource)
					  .WithImage(VarnishContainerImageTags.Image)
					  .WithImageRegistry(VarnishContainerImageTags.Registry)
					  .WithImageTag(VarnishContainerImageTags.Tag)
					  .WithEnvironment(context =>
					  {
						  context.EnvironmentVariables["VARNISH_BACKEND_HOST"] = "host.docker.internal";
						  context.EnvironmentVariables["VARNISH_BACKEND_PORT"] = "5249";
						  /* context.EnvironmentVariables[AddressEnvVarName] = $":{MinioPort}";
						  context.EnvironmentVariables[ConsoleAddressEnvVarName] = $":{MinioConsolePort}";
						  context.EnvironmentVariables[UserEnvVarName] = MinioUser;
						  context.EnvironmentVariables[PasswordEnvVarName] = MinioPassword; */
					  })
					  .WithHttpEndpoint(targetPort: 80, name: VarnishResource.PrimaryEndpointName);
	}
}
