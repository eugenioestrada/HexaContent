using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public sealed class VarnishResource(string name) : ContainerResource(name), IResourceWithEnvironment
{
	internal const string PrimaryEndpointName = "http";

	private EndpointReference? _primaryEndpoint;

	public EndpointReference PrimaryEndpoint => _primaryEndpoint ??= new(this, PrimaryEndpointName);

	public string VarnishBackendHost { get; internal set; }
	public int VarnishBackendPort { get; internal set; }
	public string ConfigPath { get; internal set; }
}
