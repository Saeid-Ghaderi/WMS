using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Domain.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        public bool IsLocked { get; set; }

        public Location()
        {
            Id = Guid.NewGuid();
        }
    }
}
