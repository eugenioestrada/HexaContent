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
			if (enviromentValues.TryGetValue("services__proxy__http__0", out var proxy) &&
				enviromentValues.TryGetValue("services__bridge__http__0", out var bridge) &&
				Uri.TryCreate(proxy, UriKind.Absolute, out var proxyUri) &&
				Uri.TryCreate(bridge, UriKind.Absolute, out var bridgeUri))
			{
				resource.VarnishBackendHost = proxyUri.Host;
				resource.VarnishBackendPort = proxyUri.Port;

				resource.VarnishDynamicHost = bridgeUri.Host;
				resource.VarnishDynamicPort = bridgeUri.Port;

				string vclContent = $$"""
					vcl 4.1;

					backend default {
						.host = "{{resource.VarnishBackendHost}}";
						.port = "{{resource.VarnishBackendPort}}";
						.max_connections = 100;
						.probe = {
							.request =
								"HEAD / HTTP/1.1"
								"Host: localhost"
								"Connection: close"
								"User-Agent: Varnish Health Probe";
							.interval  = 10s;
							.timeout   = 5s;
							.window    = 5;
							.threshold = 3;
						}
						.connect_timeout        = 5s;
						.first_byte_timeout     = 90s;
						.between_bytes_timeout  = 2s;
					}

					backend dynamic {
						.host = "{{resource.VarnishDynamicHost}}";
						.port = "{{resource.VarnishDynamicPort}}";
						.max_connections = 100;
						.probe = {
							.request =
								"HEAD / HTTP/1.1"
								"Host: localhost"
								"Connection: close"
								"User-Agent: Varnish Health Probe";
							.interval  = 10s;
							.timeout   = 5s;
							.window    = 5;
							.threshold = 3;
						}
						.connect_timeout        = 5s;
						.first_byte_timeout     = 90s;
						.between_bytes_timeout  = 2s;
					}

					sub vcl_backend_response {
						set beresp.grace = 1h;
						set beresp.keep = 30m;
						
						set beresp.do_esi = true;
					}

					sub vcl_recv {
						if (req.url ~ "^/dynamic/") {
							set req.backend_hint = dynamic;
						} else {
							set req.backend_hint = default;
						}
						
						if (req.method != "GET" &&
							req.method != "OPTIONS") {
							return (pipe);
						}

						unset req.http.Cookie;
					}
				""";

				File.WriteAllText(Path.Combine(resource.ConfigPath, "default.vcl"), vclContent);
			}
		});

		resource.ConfigPath = Path.Combine(Path.GetTempPath(), "varnish-config");

		if (!Directory.Exists(resource.ConfigPath))
		{
			Directory.CreateDirectory(resource.ConfigPath);
		}

		return builder.AddResource(resource)
					  .WithImage(VarnishContainerImageTags.Image)
					  .WithImageRegistry(VarnishContainerImageTags.Registry)
					  .WithImageTag(VarnishContainerImageTags.Tag)
					  .WithBindMount(resource.ConfigPath, "/varnish/config", true)
					  .WithEnvironment(context =>
					  {
						  context.EnvironmentVariables["VARNISH_SIZE"] = "2G";
					  })
					  .WithHttpEndpoint(targetPort: 80, name: VarnishResource.PrimaryEndpointName)
					  .WithArgs("-t", "3600", "-f", "/varnish/config/default.vcl");

	}
}
