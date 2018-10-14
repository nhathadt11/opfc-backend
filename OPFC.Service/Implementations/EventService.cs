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
    }
}
