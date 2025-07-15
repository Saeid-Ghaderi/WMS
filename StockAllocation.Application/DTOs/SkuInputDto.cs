using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.DTOs
{
    public class SkuInputDto
    {
        public string ProductNr { get; set; }
        public int QuantityAvailable { get; set; }
        public Guid LocationId { get; set; }
    }
}
