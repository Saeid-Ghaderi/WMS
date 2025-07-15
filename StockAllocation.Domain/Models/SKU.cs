using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Domain.Models
{
    public class SKU
    {
        public Guid Id { get; set; }
        public string ProductNr { get; set; }
        public int QuantityAvailable { get; set; }
        public Guid LocationId { get; set; }

        public Location Location { get; set; }

        public SKU()
        {
            Id = Guid.NewGuid();
        }
    }
}
