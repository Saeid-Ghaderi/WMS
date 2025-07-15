using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Locations.Any())
            {
                context.Locations.AddRange(
                    new Location { Id = Guid.NewGuid() },
                    new Location { Id = Guid.NewGuid() }
                );
                context.SaveChanges();
            }

            if (!context.SKUs.Any())
            {
                context.SKUs.AddRange(
                    new SKU { ProductNr = "SKU001", QuantityAvailable = 100, LocationId = context.Locations.OrderBy(i=> i.Id).First().Id },
                    new SKU { ProductNr = "SKU002", QuantityAvailable = 50, LocationId = context.Locations.OrderBy(i => i.Id).Last().Id }
                );
            }

            context.SaveChanges();
        }
    }
}
