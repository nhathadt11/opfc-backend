using System;
using OPFC.Models;
namespace OPFC.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);

        Order UpdateOrder(Order order);
    }
}
