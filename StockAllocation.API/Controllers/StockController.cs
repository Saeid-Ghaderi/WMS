using Microsoft.AspNetCore.Mvc;
using StockAllocation.Application.Interfaces;

namespace StockAllocation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockAllocatorService _stockAllocatorService;
        private readonly ISkuQuantityCorrectionService _skuQuantityCorrectionService;

        public StockController(IStockAllocatorService stockAllocatorService, ISkuQuantityCorrectionService skuQuantityCorrectionService)
        {
            _stockAllocatorService = stockAllocatorService;
            _skuQuantityCorrectionService = skuQuantityCorrectionService;
        }

        [HttpPost("allocate")]
        public async Task<IActionResult> AllocateStock()
        {
            await _stockAllocatorService.AllocateStockForOrdersAsync();
            return Ok("Stock allocation completed.");
        }

        [HttpPost("correct-sku-quantity/{skuId}")]
        public async Task<IActionResult> CorrectSkuQuantity(Guid skuId, [FromBody] int correctionAmount)
        {
            await _skuQuantityCorrectionService.CorrectSkuQuantityAsync(skuId, correctionAmount);
            return Ok("SKU quantity correction completed.");
        }
    }
}
