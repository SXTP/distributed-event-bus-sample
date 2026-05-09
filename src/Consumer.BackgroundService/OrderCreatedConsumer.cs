using MassTransit;
using Shared.Events;

namespace Consumer.BackgroundService.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        Console.WriteLine($"[Email Service] {context.Message.Price} TL tutarındaki siparişiniz için {context.Message.OrderId} nolu onay maili gönderildi.");

        await Task.CompletedTask;
    }
}