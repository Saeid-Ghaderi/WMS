﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAllocation.Application.Interfaces
{
    public interface IOrderCancellationService
    {
        Task CancelOrderAsync(Guid orderId);
        Task CancelLineItemAsync(Guid orderId, Guid lineItemId);
    }
}
