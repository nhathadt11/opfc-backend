using System;
using System.Collections.Generic;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
namespace OPFC.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOpfcUow _opfcUow;

        public OrderService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public Order CreateOrder(Order order)
        {
            try
            {
                var result = _opfcUow.OrderRepository.CreateOrder(order);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteOrder(Order order)
        {
            try
            {
                return _opfcUow.OrderRepository.DeleteOrder(order);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Order> GetAllOrder()
        {
            try
            {
                return _opfcUow.OrderRepository.GetAllOrder();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Order GetOrderById(long id)
        {
            try
            {
                return _opfcUow.OrderRepository.GetOrderById(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Order UpdateOrder(Order order)
        {
            try
            {
                var result = _opfcUow.OrderRepository.UpdateOrder(order);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
