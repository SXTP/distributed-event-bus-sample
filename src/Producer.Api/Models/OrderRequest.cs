namespace Producer.Api.Models;

public record OrderRequest(Guid OrderId, decimal Price);