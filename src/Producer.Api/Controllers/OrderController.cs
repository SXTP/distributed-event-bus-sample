using MassTransit;
using MassTransit.Transports;
using Microsoft.AspNetCore.Mvc;
using Producer.Api.Models;
using Shared.Events;

namespace Producer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IPublishEndpoint _publishEndpoint) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderRequest request)
    {
        var orderEvent = new OrderCreatedEvent
        {
            OrderId = request.OrderId,
            Price = request.Price,
            CreatedDate = DateTime.UtcNow
        };

        // 2. Mesajı Event Bus'a fırlatıyoruz
        await _publishEndpoint.Publish(orderEvent);

        return Ok(new
        {
            Message = "Sipariş başarıyla kuyruğa gönderildi!",
            OrderId = orderEvent.OrderId,
            SentPrice = orderEvent.Price
        });
    }
}