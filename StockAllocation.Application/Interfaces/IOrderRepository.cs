using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<CustomerOrder>> GetPagedOrdersAsync(int pageIndex, int pageSize);
        Task<int> GetTotalOrdersCountAsync();
        Task<CustomerOrder> GetByIdAsync(Guid id);
        Task<CustomerOrder> AddAsync(CustomerOrder order);
        Task DeleteAsync(Guid id);
    }
}
