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
                //_opfcUow.EventRepository.DeleteEvent(null);
                //_opfcUow.Commit();

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public Event GetEventById(long eventId)
        {
            throw new NotImplementedException();
        }

        public List<Event> GettAllEvent()
        {
            throw new NotImplementedException();
        }

        public bool SaveEvent(Event newEvent)
        {
            var result = false;

            try
            {
                _opfcUow.EventRepository.SaveEvent(newEvent);
                _opfcUow.Commit();

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool UpdateEvent(Event modifiedEvent)
        {
            var result = false;

            try
            {
                _opfcUow.EventRepository.UpdateEvent(modifiedEvent);
                _opfcUow.Commit();

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
    }
}
