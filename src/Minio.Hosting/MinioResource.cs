using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

/// <summary>
/// Represents a Minio resource with endpoints and connection string.
/// </summary>
/// <param name="name">The name of the Minio resource.</param>
public sealed class MinioResource(string name) : ContainerResource(name), IResourceWithConnectionString, IResourceWithEnvironment
{
	internal const string PrimaryEndpointName = "http";
	internal const string ConsoleEndpointName = "console";

	private EndpointReference? _primaryEndpoint;
	private EndpointReference? _consoleEndpoint;

	/// <summary>
	/// Gets the primary endpoint for the Minio resource. This endpoint is used for host processes to Minio communication.
	/// </summary>
	public EndpointReference PrimaryEndpoint => _primaryEndpoint ??= new(this, PrimaryEndpointName);

	/// <summary>
	/// Gets the console endpoint for the Minio resource. This endpoint is used for accessing the Minio console.
	/// </summary>
	public EndpointReference ConsoleEndpoint => _consoleEndpoint ??= new(this, ConsoleEndpointName);

	/// <summary>
	/// Gets the connection string expression for the Minio resource.
	/// </summary>
	public ReferenceExpression ConnectionStringExpression =>
		ReferenceExpression.Create(
			$"Primary=http://{PrimaryEndpoint.Property(EndpointProperty.Host)}:{PrimaryEndpoint.Property(EndpointProperty.Port)};Console=http://{ConsoleEndpoint.Property(EndpointProperty.Host)}:{ConsoleEndpoint.Property(EndpointProperty.Port)}");

	/// <summary>
	/// Gets the dictionary of buckets associated with the Minio resource.
	/// </summary>
	public Dictionary<string, string> Buckets { get; } = new();

	/// <summary>
	/// Adds a bucket to the Minio resource.
	/// </summary>
	/// <param name="name">The name of the bucket.</param>
	/// <param name="bucketName">The actual bucket name.</param>
	public void AddBucket(string name, string bucketName)
	{
		Buckets.TryAdd(name, bucketName);
	}
}

