using Microsoft.AspNetCore.Mvc;
using StockAllocation.Application.Interfaces;

namespace StockAllocation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderCancellationService _cancellationService;

        public OrdersController(IOrderCancellationService cancellationService)
        {
            _cancellationService = cancellationService;
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> CancelOrder(Guid orderId)
        {
            await _cancellationService.CancelOrderAsync(orderId);
            return NoContent();
        }

        [HttpDelete("{orderId}/lineitems/{lineItemId}")]
        public async Task<IActionResult> CancelLineItem(Guid orderId, Guid lineItemId)
        {
            await _cancellationService.CancelLineItemAsync(orderId, lineItemId);
            return NoContent();
        }
    }
}
