using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAllocation.Application.DTOs;
using StockAllocation.Application.Interfaces;
using StockAllocation.Application.Pagination;
using StockAllocation.Domain.Models;
using StockAllocation.Infrastructure.Data;

namespace StockAllocation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderController : ControllerBase
    {
        private readonly ICustomerOrderService _orderService;

        public CustomerOrderController(ICustomerOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<CustomerOrderOutputDto>>> GetOrders([FromQuery] PaginationRequest paging)
        {
            var result = await _orderService.GetPagedOrdersAsync(paging);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerOrderOutputDto>> GetOrder(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerOrderOutputDto>> CreateOrder(CustomerOrderInputDto orderDto)
        {
            var created = await _orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrder), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
