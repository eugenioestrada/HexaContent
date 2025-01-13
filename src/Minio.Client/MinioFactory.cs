using Amazon.S3;
using System.Net.Mail;

namespace HexaContent.Minio.Client;

public sealed class MinioFactory : IDisposable
{
	private readonly SemaphoreSlim _semaphore = new(1, 1);
	private readonly MinioClientSettings _settings;
	private AmazonS3Client _client;

	public MinioFactory(MinioClientSettings settings)
	{
		_settings = settings;
	}

	public async Task<AmazonS3Client> GetAmazonS3ClientAsync(
		CancellationToken cancellationToken = default)
	{
		await _semaphore.WaitAsync(cancellationToken);

		try
		{
			if (_client is null)
			{
				_client = new AmazonS3Client(new AmazonS3Config
				{
					ServiceURL = _settings.Endpoint,
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

	public void Dispose()
	{
		_client?.Dispose();
		_semaphore.Dispose();
	}
}
