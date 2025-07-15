using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.DTOs
{
    public class LineItemOutputDto
    {
        public Guid Id { get; set; }
        public string ProductNr { get; set; }
        public int QuantityRequested { get; set; }
        public int QuantityAllocated { get; set; }
        public List<Guid> AllocatedSkuIds { get; set; }
    }
}
