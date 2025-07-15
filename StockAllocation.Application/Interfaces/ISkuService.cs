using StockAllocation.Application.DTOs;
using StockAllocation.Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Interfaces
{
    public interface ISkuService
    {
        Task<PaginationResponse<SkuOutputDto>> GetAllAsync(PaginationRequest paging);
        Task<SkuOutputDto> GetByIdAsync(Guid id);
        Task<SkuOutputDto> CreateAsync(SkuInputDto dto);
        Task UpdateAsync(Guid id, SkuInputDto dto);
        Task DeleteAsync(Guid id);
    }
}
