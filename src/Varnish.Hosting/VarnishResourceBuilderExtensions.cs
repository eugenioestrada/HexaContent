using Aspire.Hosting.ApplicationModel;
using Microsoft.Extensions.DependencyInjection;

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

		builder.Eventing.Subscribe<BeforeResourceStartedEvent>(resource, async (@event, ct) =>
		{			
			var enviromentValues = await resource.GetEnvironmentVariableValuesAsync();
			if (enviromentValues.TryGetValue("services__proxy__http__0", out var proxy))
			{
				var proxyUri = new Uri(proxy);
				resource.VarnishBackendHost = proxyUri.Host;
				resource.VarnishBackendPort = proxyUri.Port;
			}
		});

		return builder.AddResource(resource)
					  .WithImage(VarnishContainerImageTags.Image)
					  .WithImageRegistry(VarnishContainerImageTags.Registry)
					  .WithImageTag(VarnishContainerImageTags.Tag)
					  .WithBindMount(Path.Combine(Environment.CurrentDirectory, "config"), "/config", true)
					  .WithEnvironment(context =>
					  {
						  context.EnvironmentVariables["VARNISH_BACKEND_HOST"] = resource.VarnishBackendHost;
						  context.EnvironmentVariables["VARNISH_BACKEND_PORT"] = resource.VarnishBackendPort;
						  context.EnvironmentVariables["VARNISH_SIZE"] = "2G";
					  })
					  .WithHttpEndpoint(targetPort: 80, name: VarnishResource.PrimaryEndpointName)
					  .WithArgs("-t", "3600");
		// -f", "/config/default.vcl"
	}
}
