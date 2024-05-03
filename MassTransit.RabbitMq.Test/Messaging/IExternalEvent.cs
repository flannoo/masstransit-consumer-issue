using System.Text.Json.Serialization;

namespace MassTransit.RabbitMq.Test.Messaging;

[ExcludeFromTopology]
[ExcludeFromImplementedTypes]
public interface IExternalEvent : IDurableEventName
{
}

/// <summary>
/// Add this interface to your integration event to specify the durable event name.
/// </summary>
public interface IDurableEventName
{
    [JsonIgnore]
    string DurableEventName { get; }
}