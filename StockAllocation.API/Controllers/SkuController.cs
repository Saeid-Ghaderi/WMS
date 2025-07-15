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
    public class SkuController : ControllerBase
    {
        private readonly ISkuService _skuService;

        public SkuController(ISkuService skuService)
        {
            _skuService = skuService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginationResponse<SkuOutputDto>>> GetAll([FromQuery] PaginationRequest paging)
        {
            var result = await _skuService.GetAllAsync(paging);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SkuOutputDto>> GetById(Guid id)
        {
            var sku = await _skuService.GetByIdAsync(id);
            return Ok(sku);
        }

        [HttpPost]
        public async Task<ActionResult<SkuOutputDto>> Create(SkuInputDto dto)
        {
            var created = await _skuService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, SkuInputDto dto)
        {
            await _skuService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _skuService.DeleteAsync(id);
            return NoContent();
        }
    }
}
