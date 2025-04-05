using Amazon.S3;

namespace HexaContent.Minio.Client;

/// <summary>
/// Factory class for creating and managing Amazon S3 clients.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="MinioFactory"/> class.
/// </remarks>
/// <param name="settings">The settings for the Minio client.</param>
public sealed class MinioFactory(MinioClientSettings settings) : IDisposable
{
	private readonly SemaphoreSlim _semaphore = new(1, 1);
	private AmazonS3Client _client;
	/// <summary>
	/// Gets an instance of <see cref="AmazonS3Client"/> asynchronously.
	/// </summary>
	/// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the <see cref="AmazonS3Client"/> instance.</returns>
	public async Task<AmazonS3Client> GetAmazonS3ClientAsync(
		CancellationToken cancellationToken = default)
	{
		await _semaphore.WaitAsync(cancellationToken);

		try
		{
			if (_client is null)
			{
				_client = new AmazonS3Client(settings.AccessKey, settings.AccessSecret, new AmazonS3Config
				{
					ServiceURL = settings.Primary,
					ForcePathStyle = true
				});
			}
		}
		finally
		{
			_semaphore.Release();
		}

		return _client;
	}

	/// <summary>
	/// Gets the bucket name from the settings.
	/// </summary>
	public string BucketName { get; } = settings.BucketName;

	/// <summary>
	/// Releases the resources used by the <see cref="MinioFactory"/> class.
	/// </summary>
	public void Dispose()
	{
		_client?.Dispose();
		_semaphore.Dispose();
	}
}
