using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public bool DeleteEvent(long eventId, long userId)
        {
            var result = false;

            try
            {
                var evn = GetEventById(eventId);

                if (evn != null)
                {
                    evn.IsDeleted = true;
                    result = UpdateEvent(evn) != null;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public Event GetEventById(long eventId)
        {
            try
            {
                return _opfcUow.EventRepository.GetEventById(eventId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<Event> GettAllEvent()
        {
            try
            {
                var result = _opfcUow.EventRepository.GettAllEvent();
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Event SaveEvent(Event newEvent)
        {
            try
            {
                newEvent.IsDeleted = false;
                var result = _opfcUow.EventRepository.SaveEvent(newEvent);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Event UpdateEvent(Event modifiedEvent)
        {
            try
            {
                var result = _opfcUow.EventRepository.UpdateEvent(modifiedEvent);
                _opfcUow.Commit();

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
