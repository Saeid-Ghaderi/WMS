using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Interfaces
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetPagedLocationsAsync(int pageIndex, int pageSize);
        Task<int> GetTotalLocationsCountAsync();
        Task<Location> GetByIdAsync(Guid id);
        Task<Location> AddAsync(Location location);
        Task UpdateAsync(Location location);
        Task DeleteAsync(Guid id);
    }
}
