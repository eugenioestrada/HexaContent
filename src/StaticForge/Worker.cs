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
            await Task.Delay(1000, stoppingToken);
        }
    }
}
