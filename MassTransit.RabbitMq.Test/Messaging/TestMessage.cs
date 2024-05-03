using MassTransit.RabbitMq.Test.Publishers;

namespace MassTransit.RabbitMq.Test.Messaging;

public sealed class TestMessage : IExternalEvent
{
    public string Name { get; set; } = default!;
    public string DurableEventName => TestMessageConstants.TestMessage;
}