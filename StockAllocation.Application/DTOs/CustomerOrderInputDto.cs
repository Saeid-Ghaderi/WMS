using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.DTOs
{
    public class CustomerOrderInputDto
    {
        public bool CompleteDeliveryRequired { get; set; }
        public int Priority { get; set; }
        public List<LineItemInputDto> LineItems { get; set; }
    }
}
