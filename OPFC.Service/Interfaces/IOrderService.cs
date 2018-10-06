using System;
using OPFC.Models;
namespace OPFC.Services.Interfaces
{
    public interface IOrderService
    {
        Order CreateOrder(Order order);
        Order UpdateOrder(Order order);
    }
}
