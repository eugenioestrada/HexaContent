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
				Dictionary<string, string> keyValuePairs = new()
				{
					{ "test.html", "<html><body><h1>Test!</h1><esi:include src=\"/esi.html\"/><esi:include src=\"/dynamic/\"/></body></html>" },
					{ "section/article.html", "<html><body><h1>Article!</h1></body></html>" },
					{ "robots.txt", "robots" },
					{ "esi.html" , "<h2>ESI!</h2>" },
					{ "index.html" , "<html><body><h1>Index!</h1></body></html>" },
					{ "sitemaps/sitemap.xml", "<?xml version=\"1.0\" encoding=\"UTF-8\"?><sitemap />" }
				};

				foreach (var kvp in keyValuePairs)
				{
					var contentType = kvp.Key switch
					{
						_ when kvp.Key.EndsWith(".html") => ContentTypes.TextHtml,
						_ when kvp.Key.EndsWith(".css") => ContentTypes.TextCss,
						_ when kvp.Key.EndsWith(".xml") => ContentTypes.ApplicationXml,
						_ => ContentTypes.TextPlain
					};

					await PutContent(client, bucket, kvp.Key, kvp.Value, kvp.Key.EndsWith(".txt") ? ContentTypes.TextPlain : ContentTypes.TextHtml);
				}
			}

			await Task.Delay(1000, stoppingToken);
        }

		static async Task PutContent(AmazonS3Client client, S3Bucket bucket, string key, string content, string contentType)
		{
			await client.PutObjectAsync(new PutObjectRequest
			{
				BucketName = bucket.BucketName,
				Key = key,
				ContentType = contentType,
				CannedACL = S3CannedACL.PublicRead,
				ContentBody = content
			});
		}
	}
}
