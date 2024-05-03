using MassTransit.RabbitMq.Test.Messaging;

namespace MassTransit.RabbitMq.Test.Consumers;

public sealed class TestMessageConsumer : IConsumer<TestMessage>
{
    private readonly ILogger<TestMessageConsumer> _logger;

    public TestMessageConsumer(ILogger<TestMessageConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<TestMessage> context)
    {
        _logger.LogInformation("Message {message} consumed at {time}", context.Message.Name, DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        return Task.CompletedTask;
    }
}