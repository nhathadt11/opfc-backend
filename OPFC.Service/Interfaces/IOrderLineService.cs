using System;
using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.Services.Interfaces
{
    public interface IOrderLineService
    {
        OrderLine GetOrderLineById(long id);
        List<OrderLine> GetAllByOrderId(long orderId);
        void Cancel(long orderLineId);
        void Approve(long orderLineId);
        void MarkAsCompleted(long orderLineId);
        bool Exists(long orderLineId);
    }
}
