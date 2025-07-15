using AutoMapper;
using StockAllocation.Application.Interfaces;
using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Services
{
    public class OrderCancellationService : IOrderCancellationService
    {
        private readonly ICustomerOrderRepository _orderRepo;
        private readonly ISkuRepository _skuRepo;

        public OrderCancellationService(ICustomerOrderRepository orderRepo, ISkuRepository skuRepo)
        {
            _orderRepo = orderRepo;
            _skuRepo = skuRepo;
        }

        public async Task CancelOrderAsync(Guid orderId)
        {
            var order = await _orderRepo.GetByIdWithLineItemsAsync(orderId);
            if (order == null)
                throw new KeyNotFoundException("Order not found");

            foreach (var lineItem in order.LineItems)
            {
                await ReleaseAllocatedStockAsync(lineItem);
            }

            order.Status = "Cancelled";

            await _orderRepo.UpdateAsync(order);
        }

        public async Task CancelLineItemAsync(Guid orderId, Guid lineItemId)
        {
            var order = await _orderRepo.GetByIdWithLineItemsAsync(orderId);
            if (order == null)
                throw new KeyNotFoundException("Order not found");

            var lineItem = order.LineItems.FirstOrDefault(li => li.Id == lineItemId);
            if (lineItem == null)
                throw new KeyNotFoundException("Line item not found");

            await ReleaseAllocatedStockAsync(lineItem);

            order.LineItems.Remove(lineItem);

            await _orderRepo.UpdateAsync(order);
        }

        private async Task ReleaseAllocatedStockAsync(LineItem lineItem)
        {
            if (lineItem.QuantityAllocated > 0)
            {
                var skus = await _skuRepo.GetAvailableSkusForProductAsync(lineItem.ProductNr);
                var sku = skus.FirstOrDefault();

                if (sku != null)
                {
                    sku.QuantityAvailable += lineItem.QuantityAllocated;
                    await _skuRepo.UpdateAsync(sku);
                }

                lineItem.QuantityAllocated = 0;
            }
        }
    }
}
