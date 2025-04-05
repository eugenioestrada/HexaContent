using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public sealed class MinioBucketResource(string name, string bucketName, MinioResource parent) : Resource(name), IResourceWithParent<MinioResource>, IResourceWithConnectionString
{
	public MinioResource Parent { get; } = parent ?? throw new ArgumentNullException(nameof(parent));

	/// <summary>
	/// Gets the bucket name.
	/// </summary>
	public string BucketName { get; } = bucketName ?? throw new ArgumentNullException(nameof(bucketName));

	/// <summary>
	/// Gets the connection string expression for the S3 Bucket.
	/// </summary>
	public ReferenceExpression ConnectionStringExpression => ReferenceExpression.Create($"{Parent};BucketName={bucketName};AccessKey={MinioResourceBuilderExtensions.MinioUser};AccessSecret={MinioResourceBuilderExtensions.MinioPassword}");
}

