using MassTransit;
using MassTransit.RabbitMq.Test;
using MassTransit.RabbitMq.Test.Consumers;
using MassTransit.RabbitMq.Test.Messaging;
using MassTransit.RabbitMq.Test.Publishers;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddScoped<TestMessagePublisher>();

var rabbitMqHost = Environment.GetEnvironmentVariable("RABBITMQ__HOST") ?? "localhost";
var rabbitMqUsername = Environment.GetEnvironmentVariable("RABBITMQ__USERNAME") ?? "guest";
var rabbitMqHPassword = Environment.GetEnvironmentVariable("RABBITMQ__PASSWORD") ?? "guest";

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost, "/", h =>
        {
            h.Username(rabbitMqUsername);
            h.Password(rabbitMqHPassword);
        });
        cfg.UsePublishFilter(typeof(DurableEventNameFilter<>), context);
        cfg.AddEventPublisher<TestMessage>(TestMessageConstants.Topic);
        
        cfg.AddExchangeBinding<TestMessage, TestMessageConsumer>(context, TestMessageConstants.Topic,
            "masterdata", TestMessageConstants.TestMessage);
        
        cfg.AutoStart = true;
    });

    x.AddConsumer<TestMessageConsumer>();
});

var host = builder.Build();
host.Run();