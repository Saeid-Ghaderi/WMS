using StockAllocation.Application.Interfaces;
using StockAllocation.Domain.Enums;

namespace StockAllocation.Application.Services
{
    public class StockAllocatorService : IStockAllocatorService
    {
        private readonly ICustomerOrderRepository _orderRepo;
        private readonly ISkuRepository _skuRepo;
        private readonly IUnitOfWork _unitOfWork;

        public StockAllocatorService(ICustomerOrderRepository orderRepo, ISkuRepository skuRepo, IUnitOfWork unitOfWork)
        {
            _orderRepo = orderRepo;
            _skuRepo = skuRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task AllocateStockForOrdersAsync()
        {
            var orders = await _orderRepo.GetOrdersNeedingAllocationAsync();

            orders = orders.OrderByDescending(o => o.Priority)
                           .ThenBy(o => o.CreatedDate)
                           .ToList();

            foreach (var order in orders)
            {
                bool canAllocate = true;

                foreach (var lineItem in order.LineItems)
                {
                    var requiredQty = lineItem.QuantityRequested - lineItem.QuantityAllocated;
                    var skus = await _skuRepo.GetAvailableSkusForProductAsync(lineItem.ProductNr);

                    int allocatedQty = 0;

                    foreach (var sku in skus)
                    {
                        int allocQty = Math.Min(sku.QuantityAvailable, requiredQty - allocatedQty);
                        sku.QuantityAvailable -= allocQty;
                        allocatedQty += allocQty;

                        if (allocatedQty >= requiredQty)
                            break;
                    }

                    lineItem.QuantityAllocated += allocatedQty;

                    if (order.CompleteDeliveryRequired && lineItem.QuantityAllocated < lineItem.QuantityRequested)
                    {
                        canAllocate = false;
                        break;
                    }
                }

                if (order.CompleteDeliveryRequired && !canAllocate)
                {
                    foreach (var lineItem in order.LineItems)
                    {
                        var allocatedQty = lineItem.QuantityAllocated;
                        if (allocatedQty > 0)
                        {
                            var skus = await _skuRepo.GetAvailableSkusForProductAsync(lineItem.ProductNr);
                            foreach (var sku in skus)
                            {
                                sku.QuantityAvailable += allocatedQty;
                                break;
                            }
                        }

                        lineItem.QuantityAllocated = 0;
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
