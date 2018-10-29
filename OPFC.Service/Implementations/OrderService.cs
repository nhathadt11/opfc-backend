using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using OPFC.API.ServiceModel.Order;
using OPFC.FirebaseService;
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
                        TotalAmount = orderMenus.Aggregate((decimal)0, (acc, m) => acc + m.Price),
                        Status = "Request Approval",
                        PaypalRef = "PAY-***"
                    };
                    var createdOrdered = _opfcUow.OrderRepository.CreateOrder(order);

                    // Commit first to get real OrderId.
                    // Althought we commit here, current order did not saved into database because we are still in TransactionScope.
                    _opfcUow.Commit();

                    var orderLines = orderMenus.Map(m => new OrderLine
                    {
                        MenuId = m.Id,
                        OrderId = createdOrdered.OrderId,
                        Amount = m.Price,
                        BrandId = m.BrandId,
                        PaypalSaleRef = "SAL-***"
                    });

                    var forEvent = _opfcUow.EventRepository.GetEventById(orderRequest.EventId);
                    forEvent.OrderId = createdOrdered.OrderId;
                    _opfcUow.EventRepository.UpdateEvent(forEvent);
                    _opfcUow.Commit();

                    _opfcUow.OrderLineRepository.CreateMany(orderLines);
                    _opfcUow.Commit();

                    SendNotification(orderMenus, userId, orderRequest.EventId, createdOrdered.OrderId);
                    
                    scope.Complete();

                    return createdOrdered;
                }
            }
            catch (Exception ex)
            {
                // It will auto rollback if any exception, so wee do not need rollback manually here
                throw ex;
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

        private void SendNotification(Menu[] menuList, long userId, long eventId, long orderId)
        {
            menuList.Each(m =>
            {           
                var forEvent = _opfcUow.EventRepository.GetEventById(eventId);
                var userByBrand = GetUserByBrandId(m.BrandId);

                FirebaseService.FirebaseService.Instance.SendNotification(new OrderPayload
                {
                    FromUserId = userId,
                    FromUsername = _opfcUow.UserRepository.GetById(userId).Username,
                    ToUserId = userByBrand.Id,
                    ToUsername = _opfcUow.UserRepository.GetById(userByBrand.Id).Username,
                    Message = m.MenuName,
                    CreatedAt = DateTime.Now,
                    Data = new Dictionary<string, object>
                    {
                        { "OrderId", orderId },
                        { "MenuId", m.Id },
                        { "MenuName", m.MenuName },
                        { "Event", forEvent }
                    }
                }); 
            });
        }

        private User GetUserByBrandId(long brandId)
        {
            var foundBrand = _opfcUow.BrandRepository.GetBrandById(brandId);
            if (foundBrand == null) throw new Exception("Brand could not be found.");

            var foundUser = _opfcUow.UserRepository.GetAllUsers().Find(u => u.Id == foundBrand.UserId);
            if (foundUser == null) throw new Exception("User could not be found.");

            return foundUser;
        }

        public List<BrandOrder> GetBrandOrderByBrandId(long brandId)
        {
            var orderLineList = _opfcUow.OrderLineRepository
                .GetAll()
                .Where(ol => ol.BrandId == brandId)
                .ToList();

            var orderLinesByOrderId = orderLineList
                .GroupBy(
                    ol => ol.OrderId,
                    ol => ol,
                    (key, ol) => new { OrderId = key, OrderLines = ol.ToList() }
                ).ToList();

            var brandOrder = orderLinesByOrderId.Select(o => {
                var foundEvent = _opfcUow.EventRepository
                    .GettAllEvent()
                    .SingleOrDefault(e => e.OrderId == o.OrderId);

                if (foundEvent == null)
                {
                    throw new Exception($"Event could not be found for {o.OrderId}");
                }
            
                return new BrandOrder
                {
                    OrderNo = o.OrderId,
                    EventNo = foundEvent.Id,
                    EventName = foundEvent.EventName,
                    EventTypeName = GetEventTypeNameById(foundEvent.EventTypeId),
                    StartAt = foundEvent.StartAt,
                    EndAt = foundEvent.EndAt,
                    EventStatus = foundEvent.Status,
                    OrderStatus = GetOrderById(o.OrderId).Status,
                    ServingNumber = foundEvent.ServingNumber,
                    CityName = GetCityNameById(foundEvent.CityId),
                    DistrictName = GetDistrictNameById(foundEvent.DistrictId),
                    Address = foundEvent.Address,
                    TotalPrice = SumOrderLineAmountByIds(o.OrderLines.Select(ol => ol.Id).ToArray()),
                    BrandOderLineList = o.OrderLines.Map(ol => ol.Id).Select(OrderLineToBrandOrderLineById).ToList()
                };
            }).ToList();

            return brandOrder;
        }
        
        private string GetEventTypeNameById(long id)
        {
            return _opfcUow.EventTypeRepository
                .GetAllEventType()
                .FirstOrDefault(et => et.Id == id)
                ?.EventTypeName;
        }
        
        private string GetCityNameById(long id)
        {
            return _opfcUow.CityRepository
                .GetAll()
                .SingleOrDefault(c => c.Id == id)
                ?.Name;
        }
        
        private string GetDistrictNameById(long id)
        {
            return _opfcUow.DistrictRepository
                .GetAll()
                .SingleOrDefault(d => d.Id == id)
                ?.Name;
        }
        
        private decimal SumOrderLineAmountByIds(long[] ids)
        {
            var orderLines = _opfcUow.OrderLineRepository
                .GetAll()
                .Where(ol => ids.Contains(ol.Id)).ToList();
                
            var aggregate = orderLines.Aggregate((decimal)0, (acc, m) => acc + m.Amount);

            return aggregate;
        }
        
        private BrandOrderLine OrderLineToBrandOrderLineById(long orderLineId)
        {
            var foundOrderLine = _opfcUow.OrderLineRepository
                .GetAll()
                .SingleOrDefault(ol => ol.Id == orderLineId);

            return new BrandOrderLine
            {
                MenuId = foundOrderLine.MenuId,
                MenuName = GetMenuNameById(foundOrderLine.Id),
                Note = foundOrderLine.Note,
                Price = foundOrderLine.Amount,
                Status = foundOrderLine.Status
            };
        }
        
        private string GetMenuNameById(long id)
        {
            return _opfcUow.MenuRepository
                .GetAll()
                .SingleOrDefault(m => m.Id == id)
                ?.MenuName;
        }

        public List<EventPlannerOrder> GetEventPlannerOrders(long userId)
        {
            var orderList = _opfcUow.OrderRepository
                .GetAllOrder()
                .Where(o => o.UserId == userId);

            var eventPlannerOrderList = orderList.Select(o => {
                var foundEvent = _opfcUow.EventRepository
                    .GettAllEvent()
                    .SingleOrDefault(e => e.OrderId == o.OrderId);
                if (foundEvent == null)
                {
                    throw new Exception($"Event could not be found for orderId {o.OrderId}");
                }

                var eventPlannerOrderLineList = _opfcUow.OrderLineRepository
                    .GetAll()
                    .Where(ol => ol.OrderId == o.OrderId).AsEnumerable()
                    .Select(ToEventPlannerOrderLine)
                    .ToList();

                return new EventPlannerOrder
                {
                    OrderNo = o.OrderId,
                    EventNo = foundEvent.Id,
                    EventName = foundEvent.EventName,
                    EventTypeName = GetEventTypeNameById(foundEvent.EventTypeId),
                    MenuNumber = eventPlannerOrderLineList.Count(),
                    StartAt = foundEvent.StartAt,
                    EndAt = foundEvent.EndAt,
                    OrderAt = o.DateOrdered,
                    Note = o.Note,
                    OrderStatus = o.Status,
                    ServingNumber = foundEvent.ServingNumber,
                    TotalPrice = o.TotalAmount,
                    OrderLineList = eventPlannerOrderLineList,
                };
            }).ToList();

            return eventPlannerOrderList;
        }
        
        private EventPlannerOrderLine ToEventPlannerOrderLine(OrderLine orderLine)
        {
            var mealList = GetAllMealByMenuId(orderLine.MenuId)
                .Select(m => new IdNameValue { Id = m.Id, Name = m.MealName })
                .ToList();

            return new EventPlannerOrderLine
            {
                OrderLineId = orderLine.Id,
                MenuId = orderLine.MenuId,
                MenuName = GetMenuNameById(orderLine.MenuId),
                BrandName = GetBrandNameById(orderLine.BrandId),
                ImageUrl = null,
                MealList = mealList,
                Note = orderLine.Note,
                Status = orderLine.Status,
                Price = orderLine.Amount,
                OtherFee = 0
            };
        }

        private string GetBrandNameById(long brandId)
        {
            return _opfcUow.BrandRepository
                .GetById(brandId)
                ?.BrandName;
        }
        
        private List<Meal> GetAllMealByMenuId(long id)
        {
            var mealIdList = _opfcUow.MenuMealRepository
                .GetByMenuId(id)
                .Select(m => m.MealId);

            var mealList = _opfcUow.MealRepository
                .GetAllMeal()
                .Where(m => mealIdList.Contains(m.Id))
                .ToList();

            return mealList;
        }
    }
}
