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
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerOrder>> GetPagedOrdersAsync(int pageIndex, int pageSize)
        {
            return await _context.CustomerOrders
                .Include(o => o.LineItems)
                .OrderByDescending(o => o.CreatedDate)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalOrdersCountAsync()
        {
            return await _context.CustomerOrders.CountAsync();
        }

        public async Task<CustomerOrder> GetByIdAsync(Guid id)
        {
            return await _context.CustomerOrders
                .Include(o => o.LineItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<CustomerOrder> AddAsync(CustomerOrder order)
        {
            _context.CustomerOrders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _context.CustomerOrders.FindAsync(id);
            if (order != null)
            {
                _context.CustomerOrders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
