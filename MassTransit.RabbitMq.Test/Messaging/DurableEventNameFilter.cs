namespace MassTransit.RabbitMq.Test.Messaging;

public sealed class DurableEventNameFilter<T> : IFilter<PublishContext<T>>
    where T : class
{
    public const string DurableEventNameHeader = "durableEventName";
    private const string _durableEventNameProperty = nameof(IDurableEventName.DurableEventName);

    public void Probe(ProbeContext context)
    {
        context.CreateFilterScope("publish.durable.eventname");
    }

    public Task Send(PublishContext<T> context, IPipe<PublishContext<T>> next)
    {
        var durableEventName = (context.Message.GetType().GetProperty(_durableEventNameProperty)?.GetValue(context.Message))
                               ?? throw new InvalidOperationException($"Message does not have the required property: {_durableEventNameProperty}" +
                                                                      $" Make sure to implement {nameof(IDurableEventName)}");

        context.Headers.Set(DurableEventNameHeader, durableEventName);

        return next.Send(context);
    }
}