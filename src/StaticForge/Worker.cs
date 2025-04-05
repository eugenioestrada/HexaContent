using Amazon.S3;
using Amazon.S3.Model;
using HexaContent.Core.Utils.Http;
using HexaContent.Minio.Client;

namespace HexaContent.StaticForge;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
	private readonly MinioFactory _minioFactory;

	public Worker(ILogger<Worker> logger, MinioFactory minioFactory)
    {
        _logger = logger;
        _minioFactory = minioFactory;
	}

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_logger.IsEnabled(LogLevel.Information))
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            }

            var client = await _minioFactory.GetAmazonS3ClientAsync();

            var buckets = await client.ListBucketsAsync();

            foreach (var bucket in buckets.Buckets)
			{
                await client.PutObjectAsync(new PutObjectRequest
                {
                    BucketName = bucket.BucketName,
                    Key = "test.html",
                    ContentType = ContentTypes.TextHtml,
					CannedACL = S3CannedACL.PublicRead,
					ContentBody = "<html><body><h1>Hello, World!</h1></body></html>"
				});
			}

			await Task.Delay(1000, stoppingToken);
        }
    }
}
