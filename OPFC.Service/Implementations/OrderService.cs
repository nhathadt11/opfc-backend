using System;
using System.Collections.Generic;
using System.Linq;
using OPFC.API.ServiceModel.Order;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using ServiceStack;

namespace OPFC.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOpfcUow _opfcUow;

        public OrderService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public Order CreateOrder(CreateOrderRequest orderRequest)
        {
            var tran = _opfcUow.BeginTransaction();
            try
            {
                var userId = orderRequest.UserId;
                var foundUser = _opfcUow.UserRepository.GetById(userId);
                if (foundUser == null)
                {
                    throw new Exception("User could not be found.");
                }

                var eventId = orderRequest.EventId;
                var foundEvent = _opfcUow.EventRepository.GetEventById(eventId);
                if (foundEvent == null)
                {
                    throw new Exception("Event could not be found");
                }

                var requestOrderMenuIds = orderRequest.MenuIds;
                var requestOrderMenus = _opfcUow.MenuRepository
                        .GetAllMenu()
                        .Where(m => requestOrderMenuIds.Contains(m.Id));
                var orderMenus = requestOrderMenus as Menu[] ?? requestOrderMenus.ToArray();
                
                var order = new Order
                {
                    UserId = userId,
                    DateOrdered = DateTime.Now,
                    TotalAmount = orderMenus.Aggregate((decimal) 0, (acc, m) => acc + m.Price)
                };    
                var createdOrdered = _opfcUow.OrderRepository.CreateOrder(order);

                var orderLines = orderMenus.Map(m => new OrderLine
                {
                    MenuId = m.Id,
                    OrderId = createdOrdered.OrderId,
                    Amount = m.Price
                });
                _opfcUow.OrderLineRepository.CreateMany(orderLines);

                _opfcUow.CommitTransaction(tran);

                return createdOrdered;
            }
            catch (Exception ex)
            {
                _opfcUow.RollbackTransaction(tran);
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
