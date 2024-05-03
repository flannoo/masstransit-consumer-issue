using MassTransit.Transports.Fabric;

namespace MassTransit.RabbitMq.Test.Messaging;

public static class MassTransitPublishExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddEventPublisher<TEvent>(this IRabbitMqBusFactoryConfigurator configurator, string topicName)
        where TEvent : class, IExternalEvent
    {
        configurator.Message<TEvent>(m => m.SetEntityName(topicName));
        configurator.Send<TEvent>(x =>
        {
            x.UseRoutingKeyFormatter<TEvent>(context =>
            {
                return context.Headers.Get<string>(DurableEventNameFilter<IDurableEventName>.DurableEventNameHeader);
            });
        });
        configurator.Publish<TEvent>(x =>
        {
            x.Durable = true; // default: true
            x.AutoDelete = false; // default: false
            x.ExchangeType = ExchangeType.Topic.ToString().ToLowerInvariant(); // default, allows any valid exchange type
        });

        return configurator;
    }
}