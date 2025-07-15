using StockAllocation.Application.DTOs;
using StockAllocation.Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Interfaces
{
    public interface ILocationService
    {
        Task<PaginationResponse<LocationOutputDto>> GetPagedLocationsAsync(PaginationRequest paging);
        Task<LocationOutputDto> GetLocationByIdAsync(Guid id);
        Task<LocationOutputDto> CreateLocationAsync(LocationInputDto locationDto);
        Task UpdateLocationAsync(Guid id, LocationInputDto locationDto);
        Task DeleteLocationAsync(Guid id);
        Task SetLockStatusAsync(Guid id, bool isLocked);
    }
}
