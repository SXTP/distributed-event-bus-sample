using Consumer.BackgroundService.Consumers;
using MassTransit;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

var rabbitHost = builder.Configuration["RabbitMqSettings:Host"];

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitHost, "/");

        cfg.ReceiveEndpoint("order-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});

var host = builder.Build();

await host.RunAsync();