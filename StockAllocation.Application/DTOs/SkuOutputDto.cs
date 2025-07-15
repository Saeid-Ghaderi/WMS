using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.DTOs
{
    public class SkuOutputDto
    {
        public Guid Id { get; set; }
        public string ProductNr { get; set; }
        public int QuantityAvailable { get; set; }
        public Guid LocationId { get; set; }
    }
}
