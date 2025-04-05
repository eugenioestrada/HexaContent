using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

/// <summary>
/// Represents a Minio bucket resource with a connection string and a parent Minio resource.
/// </summary>
/// <param name="name">The name of the Minio bucket resource.</param>
/// <param name="bucketName">The name of the bucket.</param>
/// <param name="parent">The parent Minio resource.</param>
public sealed class MinioBucketResource(string name, string bucketName, MinioResource parent) : Resource(name), IResourceWithParent<MinioResource>, IResourceWithConnectionString
{
	/// <summary>
	/// Gets the parent Minio resource.
	/// </summary>
	public MinioResource Parent { get; } = parent ?? throw new ArgumentNullException(nameof(parent));

	/// <summary>
	/// Gets the bucket name.
	/// </summary>
	public string BucketName { get; } = bucketName ?? throw new ArgumentNullException(nameof(bucketName));

	/// <summary>
	/// Gets the connection string expression for the Minio bucket.
	/// </summary>
	public ReferenceExpression ConnectionStringExpression => ReferenceExpression.Create($"{Parent};BucketName={bucketName};AccessKey={MinioResourceBuilderExtensions.MinioUser};AccessSecret={MinioResourceBuilderExtensions.MinioPassword}");
}

