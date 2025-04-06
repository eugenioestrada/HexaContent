using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public sealed class VarnishResource(string name) : ContainerResource(name), IResourceWithEnvironment
{
	internal const string PrimaryEndpointName = "http";

	private EndpointReference? _primaryEndpoint;

	public EndpointReference PrimaryEndpoint => _primaryEndpoint ??= new(this, PrimaryEndpointName);
}
