using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Interfaces
{
    public interface ICustomerOrderRepository
    {
        Task<List<CustomerOrder>> GetOrdersNeedingAllocationAsync();
        Task SaveChangesAsync();
        Task<CustomerOrder> GetByIdWithLineItemsAsync(Guid id);
        Task UpdateAsync(CustomerOrder order);
    }
}
