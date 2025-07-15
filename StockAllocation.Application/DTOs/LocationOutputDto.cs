using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.DTOs
{
    public class LocationOutputDto
    {
        public Guid Id { get; set; }
        public bool IsLocked { get; set; }
    }
}
