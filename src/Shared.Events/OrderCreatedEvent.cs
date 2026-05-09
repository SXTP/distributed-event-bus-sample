namespace Shared.Events;

public record OrderCreatedEvent
{
    public Guid OrderId { get; init; }
    public decimal Price { get; init; }
    public DateTime CreatedDate { get; init; }
}