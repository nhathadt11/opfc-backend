using System;
using System.Collections.Generic;
using OPFC.Models;
namespace OPFC.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order CreateOrder(Order order);

        Order UpdateOrder(Order order);

        Order GetOrderById(long id);

        bool DeleteOrder(Order order);

        List<Order> GetAllOrder();
    }
}
