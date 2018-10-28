using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Services.Implementations
{
    public class EventService : IEventService
    {
        private IOpfcUow _opfcUow;

        public EventService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public void DeleteEvent(long eventId, long userId)
        {
            var aEvent = GetEventById(eventId);

            if (aEvent == null)
            {
                throw new Exception("Event could not be found.");
            }

            aEvent.IsDeleted = true;
            if (UpdateEvent(aEvent) == null)
            {
                throw new Exception("Event could not be deleted.");
            }
        }

        public Event GetEventById(long eventId)
        {
            return _opfcUow.EventRepository.GetEventById(eventId);
        }

        public List<Event> GetAllEvent()
        {
            return _opfcUow.EventRepository.GettAllEvent();
        }

        public List<Event> GetAllEventByUserId(long userId)
        {
            return _opfcUow.EventRepository
                .GettAllEvent()
                .Where(e => e.UserId == userId)
                .ToList();
        }

        public Event SaveEvent(Event newEvent)
        {
            newEvent.IsDeleted = false;
            var result = _opfcUow.EventRepository.SaveEvent(newEvent);
            _opfcUow.Commit();

            return result;
        }

        public Event UpdateEvent(Event modifiedEvent)
        {
            var result = _opfcUow.EventRepository.UpdateEvent(modifiedEvent);
            _opfcUow.Commit();

            return result;
        }

        public List<Event> FindMatchedEvent(long serviceLocation, int servingNumber, decimal price, long[] eventTypeIds)
        {
            try
            {

                return _opfcUow.EventRepository.FindMatchedEvent(serviceLocation, servingNumber, price, eventTypeIds);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<List<Menu>> GetSuggestion(long eventId)
        {

            var basedEvent = _opfcUow.EventRepository.GetEventById(eventId);

            if (basedEvent == null) throw new Exception("Event not found!");

            // Get all existed Menu
            var existingMenus = _opfcUow.MenuRepository.GetAllMenuWithCollaborative();

            // List out menu id which matched event type id
            var listMenuIdMatchedEventType = _opfcUow.MenuEventTypeRepository.GetAll()
                                                                 .Where(x => x.IsDeleted == false &&
                                                                        x.EventTypeId == basedEvent.EventTypeId)
                                                                 .Select(x => x.MenuId)
                                                                 .ToList();

            // List out list menu which matched event type
            var matchedMenus = existingMenus.Where(m => listMenuIdMatchedEventType.Contains(m.Id)).ToList();

            // Get all service location by event district Id
            var listBrandIdMatchedDistrictId = _opfcUow.ServiceLocationRepository.GetServiceLocationByDistrictId(basedEvent.DistrictId)
                                                                        .Select(sl => sl.BrandId)
                                                                        .ToList();


            matchedMenus = matchedMenus.Where(m => listBrandIdMatchedDistrictId.Contains(m.BrandId)).ToList();

            matchedMenus = matchedMenus.Where(m => m.ServingNumber >= basedEvent.ServingNumber).ToList();

            var groupMenuIds = matchedMenus.Select(m => m.Id).Distinct().ToList();

            var ratingsGroup = _opfcUow.RatingRepository.GetAllRatingByMenuId(groupMenuIds)
                                                   .GroupBy(x => x.MenuId)
                                                   .ToList();

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

            var matchPercent = 0.0;

            var matchedMenuWithPercent = new Dictionary<string, double>();

            foreach (var item in combinedMenu)
            {
                var values = item.Value.Split(',').ToList();

                values.ForEach(v =>
                {
                    if (eventCategories.Contains(Int64.Parse(v)))
                    {
                        matchPercent += percentForEachTag;
                    }
                });

                matchedMenuWithPercent.Add(item.Key, matchPercent);
                matchPercent = 0.0;
            }

            var listPairedKeys = matchedMenuWithPercent.Keys.ToList();

            var finalResult = new List<List<Menu>>();

            listPairedKeys.ForEach(v => {
                var ids = v.Split(";").ToList();
                var mn = matchedMenus.Where(m => ids.Contains(m.Id.ToString())).ToList();

                finalResult.Add(mn);
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
    }
}
