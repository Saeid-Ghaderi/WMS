using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Interfaces
{
    public interface ISkuQuantityCorrectionService
    {
        Task CorrectSkuQuantityAsync(Guid skuId, int newQuantity);
    }
}
