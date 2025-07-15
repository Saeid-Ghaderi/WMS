using Microsoft.EntityFrameworkCore;
using StockAllocation.Application.Interfaces;
using StockAllocation.Domain.Models;
using StockAllocation.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Infrastructure.Repositories
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerOrder> GetByIdWithLineItemsAsync(Guid id)
        {
            return await _context.CustomerOrders
                .Include(o => o.LineItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateAsync(CustomerOrder order)
        {
            _context.CustomerOrders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CustomerOrder>> GetOrdersNeedingAllocationAsync()
        {
            return await _context.CustomerOrders
                .Include(o => o.LineItems)
                .Where(o => o.Status == "Pending")
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
