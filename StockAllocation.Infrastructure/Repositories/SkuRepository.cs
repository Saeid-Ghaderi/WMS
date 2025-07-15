using Microsoft.EntityFrameworkCore;
using StockAllocation.Application.Interfaces;
using StockAllocation.Domain.Models;
using StockAllocation.Infrastructure.Data;

namespace StockAllocation.Infrastructure.Repositories
{
    public class SkuRepository : ISkuRepository
    {
        private readonly ApplicationDbContext _context;

        public SkuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SKU>> GetAllAsync(int skip, int take)
        {
            return await _context.SKUs
                .Include(s => s.Location)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.SKUs.CountAsync();
        }

        public async Task<SKU> GetByIdAsync(Guid id)
        {
            return await _context.SKUs
                .Include(s => s.Location)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(SKU sku)
        {
            await _context.SKUs.AddAsync(sku);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SKU sku)
        {
            _context.SKUs.Update(sku);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SKU sku)
        {
            _context.SKUs.Remove(sku);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SKU>> GetAvailableSkusForProductAsync(string productNr)
        {
            return await _context.SKUs
                .Where(s => s.ProductNr == productNr && s.QuantityAvailable > 0)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
