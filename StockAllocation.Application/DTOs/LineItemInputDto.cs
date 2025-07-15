using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.DTOs
{
    public class LineItemInputDto
    {
        public string ProductNr { get; set; }
        public int QuantityRequested { get; set; }
    }
}
