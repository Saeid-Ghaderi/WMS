using Microsoft.EntityFrameworkCore;
using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<SKU> SKUs { get; set; }
        public DbSet<Location> Locations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
