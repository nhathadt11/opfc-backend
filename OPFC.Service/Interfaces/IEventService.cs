using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Interfaces
{
    public interface IEventService
    {
        Event SaveEvent(Event newEvent);

        Event UpdateEvent(Event modifiedEvent);

        void DeleteEvent(long eventId, long userId);

        Event GetEventById(long eventId);

        List<Event> GetAllEvent();

        List<Event> GetAllEventByUserId(long userId);

        List<Event> FindMatchedEvent(long serviceLocation, int servingNumber, decimal price, long[] eventTypeIds);

        List<List<Menu>> GetSuggestion(Event basedEvent);
    }
}
