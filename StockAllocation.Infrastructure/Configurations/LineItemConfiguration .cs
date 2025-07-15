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
    public class LineItemConfiguration : IEntityTypeConfiguration<LineItem>
    {
        public void Configure(EntityTypeBuilder<LineItem> builder)
        {
            builder.HasKey(li => li.Id);

            builder.Property(li => li.ProductNr)
                .IsRequired();

            builder.Property(li => li.QuantityRequested)
                .IsRequired();

            builder.Property(li => li.QuantityAllocated)
                .IsRequired();

            builder.Ignore(li => li.AllocatedSkuIds);
        }
    }
}
