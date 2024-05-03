using MassTransit.RabbitMq.Test.Publishers;

namespace MassTransit.RabbitMq.Test;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var testMessagePublisher = scope.ServiceProvider.GetRequiredService<TestMessagePublisher>();
                await testMessagePublisher.Publish();
            }
            
            await Task.Delay(10000, stoppingToken);
        }
    }
}