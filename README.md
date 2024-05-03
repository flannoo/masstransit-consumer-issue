# RabbitMQ Consumer Issue

This .NET worker application was created to demonstrate an issue with the MassTransit RabbitMQ package.
The worker will publish a message every 10 seconds to a RabbitMq queue and a consumer is configured to pick up messages from that queue.

The application works fine on MassTransit v8.2.0, with these package references:

```
<PackageReference Include="MassTransit" Version="8.2.0" />
<PackageReference Include="MassTransit.RabbitMQ" Version="8.2.0" />
```

When running the application, you will notice log messages for both the publish and consumer:

```
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Publishers.TestMessagePublisher[0]
masstransit.rabbitmq.test-1  |       Message 06a9ca60-dd2a-460b-b3b4-6647efbc657c published at: 2024-05-03 14:25:04.981
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Consumers.TestMessageConsumer[0]
masstransit.rabbitmq.test-1  |       Message 06a9ca60-dd2a-460b-b3b4-6647efbc657c consumed at 2024-05-03 14:25:04.985
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Worker[0]
masstransit.rabbitmq.test-1  |       Worker running at: 2024-05-03 14:25:14.993
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Publishers.TestMessagePublisher[0]
masstransit.rabbitmq.test-1  |       Message 03ec75a6-b104-4d2e-b704-bc04a27572e0 published at: 2024-05-03 14:25:14.996
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Consumers.TestMessageConsumer[0]
masstransit.rabbitmq.test-1  |       Message 03ec75a6-b104-4d2e-b704-bc04a27572e0 consumed at 2024-05-03 14:25:14.999
```

But on version 8.2.2, the consumer is no longer picking up messages from the queue.

```
<PackageReference Include="MassTransit" Version="8.2.2" />
<PackageReference Include="MassTransit.RabbitMQ" Version="8.2.2" />
```

When running the application using this version, you will only notice log messages for the events. The consumer is no longer triggered:

```
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Worker[0]
masstransit.rabbitmq.test-1  |       Worker running at: 2024-05-03 14:18:01.983
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Publishers.TestMessagePublisher[0]
masstransit.rabbitmq.test-1  |       Message d9e4272b-bd5e-420f-b525-7649142e3098 published at: 2024-05-03 14:18:01.986
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Worker[0]
masstransit.rabbitmq.test-1  |       Worker running at: 2024-05-03 14:18:11.988
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Publishers.TestMessagePublisher[0]
masstransit.rabbitmq.test-1  |       Message 7f2ded88-931c-4564-adff-66fffa8265c7 published at: 2024-05-03 14:18:11.988
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Worker[0]
masstransit.rabbitmq.test-1  |       Worker running at: 2024-05-03 14:18:21.977
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Publishers.TestMessagePublisher[0]
masstransit.rabbitmq.test-1  |       Message 0b611b3a-23d8-4582-bb4e-da42205237a4 published at: 2024-05-03 14:18:21.978
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Worker[0]
masstransit.rabbitmq.test-1  |       Worker running at: 2024-05-03 14:18:31.986
masstransit.rabbitmq.test-1  | info: MassTransit.RabbitMq.Test.Publishers.TestMessagePublisher[0]
masstransit.rabbitmq.test-1  |       Message 487f1476-7e5c-4cbc-8a58-dbfad681f0bc published at: 2024-05-03 14:18:31.986
```

## Run the application locally

A RabbitMq instance needs to run locally (or remotely) to run the application.
The RabbitMq hostname and credentials can be updated in the `program.cs` on lines 11, 12 & 13, in the fallback (unless you configure it as environment variables)

## Run the application using docker compose

You can also run the application using the docker compose, which will run a RabbitMQ instance and the application.
Make sure docker and docker-compose is installed and run these commands in a terminal from the solution directory:

```
docker compose build
docker compose up
```

Make sure to run the `docker compose build` command again after making changes to the project (when updating package version)
