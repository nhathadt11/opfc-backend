using System;
using System.Collections.Generic;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);
        Order UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        List<Order> GetAllOrder();
        Order GetOrderById(long id);
    }
}
