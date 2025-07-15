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
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _context;

        public LocationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Location>> GetPagedLocationsAsync(int pageIndex, int pageSize)
        {
            return await _context.Locations
                .OrderByDescending(l => l.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalLocationsCountAsync()
        {
            return await _context.Locations.CountAsync();
        }

        public async Task<Location> GetByIdAsync(Guid id)
        {
            return await _context.Locations.FindAsync(id);
        }

        public async Task<Location> AddAsync(Location location)
        {
            _context.Locations.Add(location);
            await _context.SaveChangesAsync();
            return location;
        }

        public async Task UpdateAsync(Location location)
        {
            _context.Locations.Update(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location != null)
            {
                _context.Locations.Remove(location);
                await _context.SaveChangesAsync();
            }
        }
    }
}
