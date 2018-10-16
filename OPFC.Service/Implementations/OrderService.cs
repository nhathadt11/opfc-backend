using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
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
            try
            {
                using (var scope = new TransactionScope())
                {
                    var userId = orderRequest.UserId;
                    var isUserExist = _opfcUow.UserRepository.IsUserExist(userId);

                    if (!isUserExist) throw new Exception("User could not be found.");

                    var isEventExist = _opfcUow.EventRepository.IsEventExist(orderRequest.EventId);
                    if (!isEventExist) throw new Exception("Event could not be found");

                    var requestOrderMenuIds = orderRequest.MenuIds;
                    var requestOrderMenus = _opfcUow.MenuRepository
                                                    .GetAllMenu()
                                                    .Where(m => requestOrderMenuIds.Contains(m.Id));

                    var orderMenus = requestOrderMenus as Menu[] ?? requestOrderMenus.ToArray();

                    var order = new Order
                    {
                        UserId = userId,
                        DateOrdered = DateTime.Now,
                        TotalAmount = orderMenus.Aggregate((decimal)0, (acc, m) => acc + m.Price)
                    };
                    var createdOrdered = _opfcUow.OrderRepository.CreateOrder(order);

                    // Commit first to get real OrderId.
                    // Althought we commit here, current order did not saved into database because we are still in TransactionScope.
                    _opfcUow.Commit();

                    var orderLines = orderMenus.Map(m => new OrderLine
                    {
                        MenuId = m.Id,
                        OrderId = createdOrdered.OrderId,
                        Amount = m.Price
                    });

                    _opfcUow.OrderLineRepository.CreateMany(orderLines);
                    _opfcUow.Commit();

                    scope.Complete();

                    return createdOrdered;
                }
            }
            catch (Exception ex)
            {
                // It will auto rollback if any exception, so wee do not need rollback manually here
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
