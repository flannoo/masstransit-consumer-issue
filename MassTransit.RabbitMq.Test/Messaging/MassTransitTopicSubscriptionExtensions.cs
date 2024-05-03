using MassTransit.Transports.Fabric;

namespace MassTransit.RabbitMq.Test.Messaging;

public static class MassTransitTopicSubscriptionExtensions
{
    public static IRabbitMqBusFactoryConfigurator AddExchangeBinding<TEvent, TConsumer>(this IRabbitMqBusFactoryConfigurator builder,
        IBusRegistrationContext context,
        string topic,
        string subscriptionNamePrefix,
        string routingKey)
        where TEvent : class, IExternalEvent
        where TConsumer : class, IConsumer<TEvent>
    {
        builder.ReceiveEndpoint($"{subscriptionNamePrefix}.{routingKey}", endpoint =>
        {
            endpoint.ConfigureConsumeTopology = false;
            endpoint.Bind(
                topic,
                e =>
                {
                    e.Durable = true;
                    e.AutoDelete = false;
                    e.ExchangeType = ExchangeType.Topic.ToString().ToLowerInvariant();
                    e.RoutingKey = routingKey;
                }
            );

            endpoint.ConfigureConsumer<TConsumer>(context);
        });

        return builder;
    }
}