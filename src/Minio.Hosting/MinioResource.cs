using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public sealed class MinioResource(string name) : ContainerResource(name), IResourceWithConnectionString, IResourceWithEnvironment
{
	internal const string PrimaryEndpointName = "http";
	internal const string ConsoleEndpointName = "console";


	private EndpointReference? _primaryEndpoint;
	private EndpointReference? _consoleEndpoint;

	/// <summary>
	/// Gets the primary endpoint for the Kafka broker. This endpoint is used for host processes to Kafka broker communication.
	/// To connect to the Kafka broker from a host process, use <see cref="InternalEndpoint"/>.
	/// </summary>
	public EndpointReference PrimaryEndpoint => _primaryEndpoint ??= new(this, PrimaryEndpointName);

	/// <summary>
	/// Gets the internal endpoint for the Kafka broker. This endpoint is used for container to broker communication.
	/// To connect to the Kafka broker from a host process, use <see cref="PrimaryEndpoint"/>.
	/// </summary>
	public EndpointReference ConsoleEndpoint => _consoleEndpoint ??= new(this, ConsoleEndpointName);

	public ReferenceExpression ConnectionStringExpression =>
		ReferenceExpression.Create(
			$"http://{PrimaryEndpoint.Property(EndpointProperty.Host)}:{PrimaryEndpoint.Property(EndpointProperty.Port)}");

}

