using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Interfaces
{
    public interface ISkuRepository
    {
        Task<List<SKU>> GetAllAsync(int skip, int take);
        Task<int> GetCountAsync();
        Task<SKU> GetByIdAsync(Guid id);
        Task AddAsync(SKU sku);
        Task UpdateAsync(SKU sku);
        Task DeleteAsync(SKU sku);
        Task<List<SKU>> GetAvailableSkusForProductAsync(string productNr);
    }
}
