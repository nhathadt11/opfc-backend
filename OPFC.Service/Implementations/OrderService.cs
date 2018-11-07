using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using OPFC.API.ServiceModel.Order;
using OPFC.Constants;
using OPFC.FirebaseService;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using OPFC.Services.UnitOfWork;
using ServiceStack;

namespace OPFC.Services.Implementations
{
    public class OrderService : IOrderService
    {
        readonly IServiceUow _serviceUow = ServiceStackHost.Instance.TryResolve<IServiceUow>();
        readonly IOpfcUow _opfcUow;

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

                    var requestOrderMenuIds = orderRequest.RequestMenuList.Select(m => m.MenuId);
                    var requestOrderMenus = _opfcUow.MenuRepository
                                                    .GetAllMenu()
                                                    .Where(m => requestOrderMenuIds.Contains(m.Id));

                    var orderMenus = requestOrderMenus as Menu[] ?? requestOrderMenus.ToArray();

                    // Order
                    var order = new Order
                    {
                        UserId = userId,
                        EventId = orderRequest.EventId,
                        DateOrdered = DateTime.Now,
                        TotalAmount = orderMenus.Aggregate((decimal)0, (acc, m) => acc + m.Price),
                        Status = (int)OrderStatus.Requesting,
                        PaypalRef = orderRequest.PaymentId,
                        PaypalSaleRef = orderRequest.SaleId,
                        IsDeleted = false
                    };
                    var createdOrdered = _opfcUow.OrderRepository.CreateOrder(order);

                    // Commit first to get real OrderId.
                    // Althought we commit here, current order did not saved into database because we are still in TransactionScope.
                    _opfcUow.Commit();

                    var menuByBrandId = orderMenus.GroupBy(
                        ol => ol.BrandId,
                        ol => ol,
                        (key, ol) => new { BrandId = key, MenuList = ol.ToList().CreateCopy() }
                    ).ToList();

                    menuByBrandId.ForEach(b =>
                    {
                        var amount = b.MenuList.Aggregate((decimal)0, (acc, m) => acc + m.Price);

                        // OrderLine
                        var orderLine = new OrderLine
                        {
                            OrderId = createdOrdered.OrderId,
                            BrandId = b.BrandId,
                            Amount = amount,
                            AmountEarned = amount * (decimal)(100 - AppSettings.Rate),
                            Status = (int)OrderStatus.Requesting
                        };
                        var createdOrderLine = _opfcUow.OrderLineRepository.Create(orderLine);
                        _opfcUow.Commit();

                        var orderLineDetails = b.MenuList.Map(m => new OrderLineDetail
                        {
                            OrderLineId = createdOrderLine.Id,
                            MenuId = m.Id,
                            Quantity = 1,
                            Amount = m.Price,

                        }).ToList();
                        _opfcUow.OrderLineDetailRepository.CreateRange(orderLineDetails);
                        _opfcUow.Commit();

                        // notification
                        SendNotification(orderLine, userId, orderRequest.EventId, createdOrdered.OrderId);
                    });

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

        private void SendNotification(OrderLine orderLine, long userId, long eventId, long orderId)
        {
            var forEvent = _opfcUow.EventRepository.GetEventById(eventId);
            var userByBrand = GetUserByBrandId(orderLine.BrandId);
            var fromUsername = _opfcUow.UserRepository.GetById(userId).Username;

            FirebaseService.FirebaseService.Instance.SendNotification(new OrderPayload
            {
                FromUserId = userId,
                FromUsername = fromUsername,
                ToUserId = userByBrand.Id,
                ToUsername = _opfcUow.UserRepository.GetById(userByBrand.Id).Username,
                Message = forEvent.EventName,
                Subject = fromUsername,
                Verb = "requested for",
                Object = forEvent.EventName,
                CreatedAt = DateTime.Now,
                Read = false,
                Data = new Dictionary<string, object>
                {
                    { "OrderId", orderId },
                    { "Event", forEvent }
                }
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

            var brandOrder = orderLineList.Select(ol =>
            {
                var foundOrder = _opfcUow.OrderRepository.GetById(ol.OrderId);
                if (foundOrder == null)
                {
                    throw new Exception("Order could not be found.");
                }

                var foundEvent = _opfcUow.EventRepository
                    .GettAllEvent()
                    .SingleOrDefault(e => e.Id == foundOrder.EventId);
                if (foundEvent == null)
                {
                    throw new Exception($"Event could not be found for {ol.OrderId}.");
                }

                var orderLineDetails = _opfcUow.OrderLineDetailRepository.GetAllByOrderLineId(ol.Id);

                return new BrandOrder
                {
                    OrderNo = ol.Id,
                    EventNo = foundEvent.Id,
                    EventName = foundEvent.EventName,
                    EventTypeName = GetEventTypeNameById(foundEvent.EventTypeId),
                    StartAt = foundEvent.StartAt,
                    EndAt = foundEvent.EndAt,
                    EventStatus = ((EventStatus)foundEvent.Status).ToString("F"),
                    OrderStatus = ((OrderStatus)ol.Status).ToString("F"),
                    ServingNumber = foundEvent.ServingNumber,
                    CityName = GetCityNameById(foundEvent.CityId),
                    DistrictName = GetDistrictNameById(foundEvent.DistrictId),
                    Address = foundEvent.Address,
                    TotalAmount = ol.Amount,
                    TotalAmountEarned = ol.AmountEarned,
                    BrandOderLineList = orderLineDetails.Select(OrderLineToBrandOrderLineById).ToList()
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

        private BrandOrderLine OrderLineToBrandOrderLineById(OrderLineDetail orderLineDetail)
        {
            return new BrandOrderLine
            {
                MenuId = orderLineDetail.MenuId,
                MenuName = GetMenuNameById(orderLineDetail.MenuId),
                Note = orderLineDetail.Note,
                Price = orderLineDetail.Amount
            };
        }

        private string GetMenuNameById(long id)
        {
            return _opfcUow.MenuRepository
                .GetAll()
                .SingleOrDefault(m => m.Id == id)
                ?.MenuName;
        }

        public EventPlannerOrder GetEventPlannerOrderById(long orderId)
        {
            var foundOrder = _opfcUow.OrderRepository
                .GetOrderById(orderId);
            var foundEvent = _opfcUow.EventRepository
                .GettAllEvent()
                .SingleOrDefault(e => e.Id == foundOrder.EventId);
            var orderLineList = _opfcUow.OrderLineRepository
                .GetAll()
                .Where(ol => ol.OrderId == orderId)
                .AsEnumerable()
                .SelectMany(ol =>
                {
                    var orderLineDetailList = _opfcUow.OrderLineDetailRepository.GetAllByOrderLineId(ol.Id).ToList();
                    return orderLineDetailList.Select(old => { old.BrandName = GetBrandNameById(ol.BrandId); return old; });
                })
                .Map(ToEventPlannerOrderLineDetail);

            return new EventPlannerOrder
            {
                OrderNo = foundOrder.OrderId,
                EventNo = foundEvent.Id,
                EventName = foundEvent.EventName,
                EventTypeName = GetEventTypeNameById(foundEvent.EventTypeId),
                MenuNumber = orderLineList.Count(),
                StartAt = foundEvent.StartAt,
                EndAt = foundEvent.EndAt,
                OrderAt = foundOrder.DateOrdered,
                Note = foundOrder.Note,
                OrderStatus = ((OrderStatus)foundOrder.Status).ToString("F"),
                EventStatus = ((EventStatus)foundEvent.Status).ToString("F"),
                ServingNumber = foundEvent.ServingNumber,
                TotalPrice = foundOrder.TotalAmount,
                OrderLineList = orderLineList,
            };
        }

        public List<EventPlannerOrder> GetEventPlannerOrders(long userId)
        {
            var orderList = _opfcUow.OrderRepository
                .GetAllOrder()
                .Where(o => o.UserId == userId);

            var eventPlannerOrderList = orderList.Select(o =>
            {
                var foundEvent = _opfcUow.EventRepository
                    .GettAllEvent()
                    .SingleOrDefault(e => e.Id == o.EventId);
                if (foundEvent == null)
                {
                    throw new Exception($"Event could not be found for orderId {o.OrderId}");
                }

                var orderLineDetailCountByOrderLineId = _opfcUow.OrderLineRepository
                    .GetAllByOrderId(o.OrderId)
                    .AsEnumerable()
                    .Select(ol =>
                    {
                        var orderLineDetailList = _opfcUow.OrderLineDetailRepository.GetAllByOrderLineId(ol.Id);
                        return orderLineDetailList.Count;
                    })
                    .Aggregate(0, (acc, cur) => acc + cur);

                return new EventPlannerOrder
                {
                    OrderNo = o.OrderId,
                    EventNo = foundEvent.Id,
                    EventName = foundEvent.EventName,
                    EventTypeName = GetEventTypeNameById(foundEvent.EventTypeId),
                    MenuNumber = orderLineDetailCountByOrderLineId,
                    StartAt = foundEvent.StartAt,
                    EndAt = foundEvent.EndAt,
                    OrderAt = o.DateOrdered,
                    Note = o.Note,
                    OrderStatus = ((OrderStatus)o.Status).ToString("F"),
                    ServingNumber = foundEvent.ServingNumber,
                    TotalPrice = o.TotalAmount
                };
            }).ToList();

            return eventPlannerOrderList;
        }

        private EventPlannerOrderLine ToEventPlannerOrderLineDetail(OrderLineDetail orderLineDetail)
        {
            var mealList = GetAllMealByMenuId(orderLineDetail.MenuId)
                .Select(m => new IdNameValue { Id = m.Id, Name = m.MealName })
                .ToList();

            return new EventPlannerOrderLine
            {
                OrderLineId = orderLineDetail.Id,
                MenuId = orderLineDetail.MenuId,
                MenuName = GetMenuNameById(orderLineDetail.MenuId),
                BrandName = orderLineDetail.BrandName,
                ImageUrl = null,
                MealList = mealList,
                Note = orderLineDetail.Note,
                //Status = ((OrderStatus)orderLineDetail.Status).ToString("F"),
                Price = orderLineDetail.Amount,
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

        public bool Exits(long id)
        {
            return _opfcUow.OrderRepository.GetOrderById(id) != null;
        }

        public Order GetOrderRelatedToOrderLineId(long orderLineId)
        {
            var foundOrderLine = _opfcUow.OrderLineRepository
                .GetAll()
                .SingleOrDefault(o => o.Id == orderLineId);
            if (foundOrderLine == null)
            {
                throw new Exception("OrderLine could not be found.");
            }

            return GetOrderById(foundOrderLine.OrderId);
        }

        public OrderPayload GetOrderPayloadByOrderLineId(long orderLineId)
        {
            var orderLine = _opfcUow.OrderLineRepository.GetById(orderLineId);
            
            var brandId = orderLine.BrandId;
            var brandUser = _serviceUow.UserService.GetUserByBrandId(brandId);
            var eventPlannerUser = _serviceUow.UserService.GetUserWhoMadeOrderLineId(orderLineId);
            var order = _serviceUow.OrderService.GetOrderRelatedToOrderLineId(orderLineId);
            var foundEvent = _serviceUow.EventService.GetEventRelatedToOrderId(order.OrderId);

            return new OrderPayload
            {
                FromUserId = brandUser.Id,
                FromUsername = brandUser.Username,
                ToUserId = eventPlannerUser.Id,
                ToUsername = eventPlannerUser.Username,
                CreatedAt = DateTime.Now,
                Message = $"{brandUser.Username} approved {foundEvent.EventName}",
                Subject = brandUser.Username,
                Verb = "approved",
                Object = foundEvent.EventName,
                Read = false,
                Data = new Dictionary<string, object> {
                    { "OrderId", order.OrderId },
                    { "EventId", foundEvent.Id },
                    { "EventName", foundEvent.EventName }
                }
            };
        }
    }
}
