using MassTransit.RabbitMq.Test.Messaging;

namespace MassTransit.RabbitMq.Test.Publishers;

public class TestMessagePublisher
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<TestMessagePublisher> _logger;

    public TestMessagePublisher(IServiceScopeFactory scopeFactory, ILogger<TestMessagePublisher> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    public async Task Publish()
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

            var testMessage = new TestMessage() { Name = Guid.NewGuid().ToString() };
            _logger.LogInformation("Message {message} published at: {time}", testMessage.Name, DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            
            await publishEndpoint.Publish(testMessage, typeof(TestMessage));
        }
    }
}