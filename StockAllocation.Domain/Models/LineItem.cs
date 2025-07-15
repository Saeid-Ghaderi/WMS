using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Domain.Models
{
    public class LineItem
    {
        public Guid Id { get; set; }
        public string ProductNr { get; set; }
        public int QuantityRequested { get; set; }
        public int QuantityAllocated { get; set; }
        public List<Guid> AllocatedSkuIds { get; set; } = new();

        public LineItem()
        {
            Id = Guid.NewGuid();
        }
    }
}
