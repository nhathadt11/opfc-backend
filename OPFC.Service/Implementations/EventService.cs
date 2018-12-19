using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using RedisService;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Newtonsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace OPFC.Services.Implementations
{
    public class EventService : IEventService
    {
        private IOpfcUow _opfcUow;

        public EventService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public bool IsEventExist(long eventId)
        {
            try
            {
                return _opfcUow.EventRepository.IsEventExist(eventId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DeleteEvent(long eventId, long userId)
        {
            var aEvent = _opfcUow.EventRepository.GetEventById(eventId);

            if (aEvent == null)
            {
                throw new Exception("Event could not be found.");
            }

            aEvent.IsDeleted = true;
            var result = _opfcUow.EventRepository.UpdateEvent(aEvent);
            _opfcUow.Commit();

            if (result == null)
            {
                throw new Exception("Event could not be deleted.");
            }
        }

        public Event GetEventById(long eventId)
        {
            var foundEvent = _opfcUow.EventRepository.GetEventById(eventId);
            if (foundEvent == null)
            {
                throw new Exception("Event could not be found.");
            }
            var categoryIds = _opfcUow.EventCategoryRepository
                .GetAllByEventId(eventId)
                .Select(ec => ec.CategoryId)
                .ToArray();

            foundEvent.CategoryIds = categoryIds;
            foundEvent.CategoryNames = _opfcUow.CategoryRepository
                .GetAllByIds(categoryIds.ToList())
                .Select(c => c.Name)
                .ToArray();
            foundEvent.CityName = _opfcUow.CityRepository.GetById(foundEvent.CityId)?.Name;
            foundEvent.DistrictName = _opfcUow.DistrictRepository.GetById(foundEvent.DistrictId)?.Name;

            return foundEvent;
        }

        public List<Event> GetAllEvent()
        {
            return _opfcUow.EventRepository
                .GettAllEvent()
                .Select(e =>
                {
                    e.CityName = _opfcUow.CityRepository.GetById(e.CityId)?.Name;
                    e.DistrictName = _opfcUow.DistrictRepository.GetById(e.DistrictId)?.Name;
                    e.OrderId = _opfcUow.OrderRepository.GetByEventId(e.Id)?.OrderId;
                    return e;
                })
                .ToList();
        }

        public List<Event> GetAllEventByUserId(long userId)
        {
            return GetAllEvent()
                .Where(e => e.UserId == userId)
                .ToList();
        }

        public Event SaveEvent(Event newEvent)
        {
            var result = new Event();

            newEvent.Status = (int)EventStatus.Planning;
            newEvent.IsDeleted = false;

            using (var scope = new TransactionScope())
            {
                result = _opfcUow.EventRepository.SaveEvent(newEvent);
                _opfcUow.Commit();

                var categoryIds = newEvent.CategoryIds.ToList();

                if (categoryIds != null && categoryIds.Count > 0)
                {
                    _opfcUow.EventCategoryRepository.AddMultiples(result.Id, categoryIds);
                    _opfcUow.Commit();
                }

                scope.Complete();
            }

            return GetEventById(result.Id);
        }

        public Event UpdateEvent(Event modifiedEvent)
        {
            var result = new Event();
            using (var scope = new TransactionScope())
            {
                result = _opfcUow.EventRepository.UpdateEvent(modifiedEvent);
                _opfcUow.Commit();

                var catIds = modifiedEvent.CategoryIds.ToList();

                var eventCategories = _opfcUow.EventCategoryRepository.GetAllByEventId(modifiedEvent.Id);

                eventCategories.ForEach(ec =>
                {
                    _opfcUow.EventCategoryRepository.Delete(ec);
                });

                _opfcUow.Commit();

                _opfcUow.EventCategoryRepository.AddMultiples(modifiedEvent.Id, catIds);
                _opfcUow.Commit();

                scope.Complete();
                
                // Clear suggestion result cache for that event
                RedisService.RedisService.INSTANCE.Remove(result.Id.ToString());
            }

            return GetEventById(result.Id);
        }

        public List<Event> FindMatchedEvent(long serviceLocation, int servingNumber, decimal price, long[] eventTypeIds)
        {
            return _opfcUow.EventRepository.FindMatchedEvent(serviceLocation, servingNumber, price, eventTypeIds);
        }

        public List<object> GetSuggestion(long eventId, long orderLineId = 0)
        {
            var basedEvent = _opfcUow.EventRepository.GetEventById(eventId);

            if (basedEvent == null) throw new Exception("Event not found!");

            // Get all existed Menu
            var existingMenus = _opfcUow.MenuRepository.GetAllMenuWithCollaborative();

            // List out menu id which matched event type id
            var listMenuIdMatchedEventType = _opfcUow.MenuEventTypeRepository.GetAll()
                                                                 .Where(x => x.EventTypeId == basedEvent.EventTypeId)
                                                                 .Select(x => x.MenuId)
                                                                 .ToList();

            // List out list menu which matched event type
            var matchedMenus = existingMenus.Where(m => listMenuIdMatchedEventType.Contains(m.Id)).ToList();



            // Get all service location by event district Id
            var listBrandIdMatchedDistrictId = _opfcUow.ServiceLocationRepository.GetServiceLocationByDistrictId(basedEvent.DistrictId)
                                                                        .Select(sl => sl.BrandId)
                                                                        .ToList();

            // for Cancel
            var orderLine = new OrderLine();

            if (orderLineId > 0)
            {
                orderLine = _opfcUow.OrderLineRepository.GetOrderLineById(orderLineId);

                if (orderLine != null)
                {
                    var orderLinesByOrderId = _opfcUow.OrderLineRepository.GetAllByOrderId(orderLine.OrderId);
                    var orderedMenus = _opfcUow.OrderLineDetailRepository.GetAllByOrderLineIds(orderLinesByOrderId.Select(x => x.Id).ToList()).ToList();

                    var cancelledMenuIds = orderedMenus.Where(x => x.OrderLineId == orderLineId).Select(x => x.MenuId).ToList();
                    var cancelledMenus = existingMenus.Where(m => cancelledMenuIds.Contains(m.Id)).ToList();

                    var orderedMenuIds = orderedMenus.Select(x => x.MenuId).ToList();


                    // remove cancelled menus 
                    matchedMenus.RemoveAll(x => orderedMenuIds.Contains(x.Id));

                    var cmCategoryIds = new List<long>();
                    cancelledMenus.ForEach(cm =>
                    {
                        cmCategoryIds.AddRange(cm.MenuCategoryList.Select(x => x.CategoryId));
                    });

                    var result = new List<Menu>();
                    // compare match category
                    matchedMenus.ForEach(mm =>
                    {
                        var mmCategoryIds = mm.MenuCategoryList.Select(x => x.CategoryId).ToList();
                        if (cmCategoryIds.Any(x => mmCategoryIds.Contains(x)))
                        {
                            result.Add(mm);
                        }
                    });

                    result = result.Where(x => x.BrandId == orderLine.BrandId).ToList();
                    var total = result.Count;

                    return new List<object> { result };
                }

            }
            else
            {
                matchedMenus = matchedMenus.Where(m => listBrandIdMatchedDistrictId.Contains(m.BrandId)).ToList();
            }

            var percent = basedEvent.ServingNumber * 0.07;
            var botServing = basedEvent.ServingNumber - percent;
            var topServing = basedEvent.ServingNumber + percent;

            //matchedMenus = matchedMenus.Where(m => m.ServingNumber >= basedEvent.ServingNumber).ToList();
            matchedMenus = matchedMenus.Where(m => m.ServingNumber >= botServing && m.ServingNumber <= topServing).ToList();

            //
            InjectMealListIntoMenuList(matchedMenus);

            //
            InjectCategoryListIntoMenuList(matchedMenus);

            //
            InjectBrandName(matchedMenus);

            var groupMenuIds = matchedMenus.Select(m => m.Id).Distinct().ToList();

            var ratingsGroup = _opfcUow.RatingRepository.GetAllRatingByMenuId(groupMenuIds)
                                                   .GroupBy(x => x.MenuId)
                                                   .ToList();

            // Ratings
            var avgRateByMenuId = new Dictionary<long, double>();

            foreach (var menu in ratingsGroup)
            {
                var vals = menu.Select(x => x.Rate).ToList();
                var avgRate = vals.Average();

                avgRate = avgRate * 0.3;

                avgRateByMenuId.Add(menu.Key, avgRate);
            }


            var menuCategories = _opfcUow.MenuCategoryRepository.GetAllByMenuIds(groupMenuIds)
                                                              .GroupBy(mt => mt.MenuId).ToList();

            var eventCategories = _opfcUow.EventCategoryRepository.GetAllByEventId(basedEvent.Id)
                                                                  .Select(x => x.CategoryId)
                                                                  .Distinct().ToList();


            // for testting
            //var pairs = new Dictionary<string, string>();
            //pairs.Add("39", "1,2");
            //pairs.Add("603", "3,4");
            //pairs.Add("79", "2,3");


            // This code work with our system
            var pairs = new Dictionary<string, string>();
            foreach (var item in menuCategories)
            {
                var arr = item.ToList().Select(x => x.CategoryId).ToArray();

                var val = string.Join(",", arr);

                pairs.Add(item.Key.ToString(), val);
            }

            var combinedMenu = GetCombine(pairs);

            double percentForEachTag = 100.00 / eventCategories.Count;

            var matchedMenuWithPercent = new Dictionary<string, double>();

            foreach (var item in combinedMenu)
            {
                var matchPercent = 0.0;
                var values = item.Value.Split(',').ToList();

                values.ForEach(v =>
                {
                    if (eventCategories.Contains(Int64.Parse(v)))
                    {
                        matchPercent += percentForEachTag;
                    }
                });

                matchedMenuWithPercent.Add(item.Key, matchPercent);
            }

            var listPairedKeys = matchedMenuWithPercent.Keys.ToList();

            // % Rating + % Matched
            var comboWithWeight = new Dictionary<string, double>();

            foreach (var item in matchedMenuWithPercent)
            {
                var matchedWeightValue = item.Value * 0.3;
                var combinedIds = item.Key.Split(";").ToList();
                var ratingWeight = 0.0;
                foreach (var id in combinedIds)
                {
                    var ratingWeightValue = avgRateByMenuId.Where(x => x.Key == Int64.Parse(id)).SingleOrDefault().Value;

                    ratingWeight = ratingWeightValue + matchedWeightValue;
                }

                comboWithWeight.Add(item.Key, ratingWeight);
            }

            // % Budget
            var comboWithBudget = new Dictionary<string, double>();
            comboWithWeight.ToList().ForEach(v =>
            {
                var ids = v.Key.Split(";").ToList();
                var mn = matchedMenus.Where(m => ids.Contains(m.Id.ToString())).ToList();
                var avgBud = 0.0;

                mn.ForEach(m =>
                {
                    avgBud += (double)m.Price;
                });
                //var w = v.Value + ((1 / avgBud) * 0.4);
                var w = v.Value + ((1 / Math.Abs(avgBud - (double)basedEvent.Budget)) * 0.4);
                comboWithBudget.Add(v.Key, w);
            });


            var sortComboWithWeight = comboWithBudget.OrderByDescending(x => x.Value).ToList();

            var finalResult = new List<object>();

            sortComboWithWeight.ForEach(v =>
            {
                var ids = v.Key.Split(";").ToList();
                var mn = matchedMenus.Where(m => ids.Contains(m.Id.ToString())).ToList();

                var comboPrice = (double)mn.Select(x => x.Price).ToList().Sum();

                var (Menus, ComboTotal) = (mn, comboPrice);

                // Note: this will compare ComboTotal with event Budget before add to result list
                //if (ComboTotal <= (double) basedEvent.Budget) {
                //    finalResult.Add(new { Menus, ComboTotal });
                //}

                var ComboCategoryIds = new List<long>();
                // TODO: refactor this code
                foreach (var menu in Menus)
                {
                    var categoryIds = menuCategories.Where(x => x.Key == menu.Id)
                        .Select(x => x.Select(c => c.CategoryId).ToList()).ToList();

                    if (categoryIds.Count > 0)
                    {
                        ComboCategoryIds.AddRange(categoryIds[0]);
                    }
                }

                ComboCategoryIds = ComboCategoryIds.Distinct().OrderBy(x => x).ToList();

                finalResult.Add(new MenuCombo
                    {
                        Guid = Guid.NewGuid().ToString(),
                        Menus = Menus,
                        ComboTotal = ComboTotal,
                        ComboCategoryIds = ComboCategoryIds
                    });
            });

            return finalResult;
        }

        private Dictionary<string, string> Result = new Dictionary<string, string>();

        private Dictionary<string, string> GetCombine(Dictionary<string, string> pairs)
        {

            for (int i = 0; i < pairs.Count; i++)
            {
                var newDic = new Dictionary<string, string>();

                for (int j = i; j < pairs.Count; j++)
                {
                    var elementAtI = pairs.ElementAt(i);
                    var elementAtJ = pairs.ElementAt(j);

                    if (!elementAtI.Key.Equals(elementAtJ.Key))
                    {
                        var arrNewKeyPair = (elementAtI.Key + ";" + elementAtJ.Key).ToArray();
                        var newKeyPair = new string(arrNewKeyPair);

                        var distinctKey = newKeyPair.Split(';').Distinct().ToList();
                        var cleanedNewKeyPair = string.Join(";", distinctKey).ToString();

                        var newValue = (elementAtI.Value + "," + elementAtJ.Value).ToArray();
                        var originNewValue = new string(newValue);

                        var cleanedNewValue = originNewValue.Split(',').Distinct().ToList();

                        if (!newDic.Keys.Contains(cleanedNewKeyPair))
                        {
                            newDic.Add(cleanedNewKeyPair, string.Join(",", cleanedNewValue).ToString());
                        }

                        if (!Result.Keys.Contains(cleanedNewKeyPair))
                        {
                            Result.Add(cleanedNewKeyPair, string.Join(",", cleanedNewValue).ToString());
                        }

                    }
                }

                GetCombine(newDic);
            }

            return Result;
        }

        public Event GetEventRelatedToOrderId(long orderId)
        {
            var foundOrder = _opfcUow.OrderRepository.GetById(orderId);
            if (foundOrder == null)
            {
                throw new Exception("Event could not be found.");
            }

            return GetEventById(foundOrder.EventId);
        }

        private void InjectMealListIntoMenuList(List<Menu> menuList)
        {
            // prepare meal list to inject into matched menu
            var mealList = _opfcUow.MealRepository.GetAllMeal();

            // inject meal list into menu
            menuList.ForEach(m =>
            {
                var listMealIds = m.MenuMealList.Select(mm => mm.MealId).Distinct().ToList();
                m.MealList = mealList.Where(mm => listMealIds.Contains(mm.Id)).ToList();
            });
        }

        private void InjectCategoryListIntoMenuList(List<Menu> menuList)
        {
            var categoryList = _opfcUow.CategoryRepository.GetAll();

            menuList.ForEach(m =>
            {
                var listCategoryIds = m.MenuCategoryList.Select(mc => mc.CategoryId).Distinct().ToList();
                m.CategoryList = categoryList.Where(c => listCategoryIds.Contains(c.Id)).ToList();
            });
        }
        
        private void InjectBrandName(List<Menu> menuList)
        {
            menuList.ForEach(m =>
            {
                m.BrandName = _opfcUow.BrandRepository.GetById(m.BrandId)?.BrandName;
            });
        }

        void CacheToRedis(string key, List<object> menuCombo)
        {
            RedisService.RedisService.INSTANCE.Set<List<object>>(key, menuCombo);
        }

        List<object> GetFromRedis(string key)
        {
            return RedisService.RedisService.INSTANCE.Get<List<object>>(key);
        }

        public List<object> GetSuggestionWithCache(long eventId, long orderLineId = 0)
        {
            try
            {
                var cachedMenuCombo = GetFromRedis(eventId.ToString());
                if (cachedMenuCombo != null)
                {
                    return cachedMenuCombo;
                }
    
                var menuCombo = GetSuggestion(eventId, orderLineId);
                
                // Cache to redis if not exists
                CacheToRedis(eventId.ToString(), menuCombo);
                
                return menuCombo;
            }
            catch (Exception ex)
            {
                // Fault tolerance in case of redis service initialization failure
                return GetSuggestion(eventId, orderLineId);
            }
        }
    }
}
