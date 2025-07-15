using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.DTOs
{
    public class CustomerOrderOutputDto
    {
        public Guid Id { get; set; }
        public bool CompleteDeliveryRequired { get; set; }
        public string Status { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<LineItemOutputDto> LineItems { get; set; }
    }
}
