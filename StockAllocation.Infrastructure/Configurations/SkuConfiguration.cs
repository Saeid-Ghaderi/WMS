using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockAllocation.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Infrastructure.Configurations
{
    public class SkuConfiguration : IEntityTypeConfiguration<SKU>
    {
        public void Configure(EntityTypeBuilder<SKU> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.ProductNr)
                .IsRequired();

            builder.Property(s => s.QuantityAvailable)
                .IsRequired();

            builder.Property(s => s.LocationId)
                .IsRequired();
        }
    }
}
