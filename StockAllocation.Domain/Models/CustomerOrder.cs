using StockAllocation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Domain.Models
{
    public class CustomerOrder
    {
        public Guid Id { get; set; }
        public List<LineItem> LineItems { get; set; } = new();
        public bool CompleteDeliveryRequired { get; set; }
        public PriorityLevel Priority { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }

        public CustomerOrder()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
            Status = "New";
        }
    }
}
