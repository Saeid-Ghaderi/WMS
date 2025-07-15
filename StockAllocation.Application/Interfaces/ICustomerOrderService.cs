using StockAllocation.Application.DTOs;
using StockAllocation.Application.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Interfaces
{
    public interface ICustomerOrderService
    {
        Task<PaginationResponse<CustomerOrderOutputDto>> GetPagedOrdersAsync(PaginationRequest paging);
        Task<CustomerOrderOutputDto> GetOrderByIdAsync(Guid id);
        Task<CustomerOrderOutputDto> CreateOrderAsync(CustomerOrderInputDto orderDto);
        Task DeleteOrderAsync(Guid id);
    }
}
