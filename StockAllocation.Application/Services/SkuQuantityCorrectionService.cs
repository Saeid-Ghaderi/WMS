using StockAllocation.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Services
{
    public class SkuQuantityCorrectionService : ISkuQuantityCorrectionService
    {
        private readonly ISkuRepository _skuRepo;
        private readonly ICustomerOrderRepository _orderRepo;
        private readonly IUnitOfWork _unitOfWork;

        public SkuQuantityCorrectionService(ISkuRepository skuRepo, ICustomerOrderRepository orderRepo, IUnitOfWork unitOfWork)
        {
            _skuRepo = skuRepo;
            _orderRepo = orderRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task CorrectSkuQuantityAsync(Guid skuId, int newQuantity)
        {
            var sku = await _skuRepo.GetByIdAsync(skuId);

            if (sku == null)
                throw new Exception("SKU not found");

            sku.QuantityAvailable = newQuantity;

            var orders = await _orderRepo.GetOrdersNeedingAllocationAsync();

            foreach (var order in orders)
            {
                foreach (var lineItem in order.LineItems)
                {
                    if (lineItem.ProductNr == sku.ProductNr)
                    {
                        if (lineItem.QuantityAllocated > newQuantity)
                        {
                            var substituteSkus = await _skuRepo.GetAvailableSkusForProductAsync(lineItem.ProductNr);

                            var substitute = substituteSkus
                                .Where(s => s.Id != skuId && s.QuantityAvailable >= lineItem.QuantityRequested)
                                .FirstOrDefault();

                            if (substitute != null)
                            {
                                // Deallocate from old SKU
                                sku.QuantityAvailable += lineItem.QuantityAllocated;

                                // Allocate from substitute
                                substitute.QuantityAvailable -= lineItem.QuantityRequested;
                                lineItem.QuantityAllocated = lineItem.QuantityRequested;
                            }
                            else
                            {
 
                                if (order.CompleteDeliveryRequired)
                                {
                                    foreach (var li in order.LineItems)
                                    {
                                        var skuList = await _skuRepo.GetAvailableSkusForProductAsync(li.ProductNr);
                                        var allocatedSku = skuList.FirstOrDefault();

                                        if (allocatedSku != null)
                                            allocatedSku.QuantityAvailable += li.QuantityAllocated;

                                        li.QuantityAllocated = 0;
                                    }

                                    order.Status = "DeallocatedDueToCorrection";
                                }
                            }
                        }
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
