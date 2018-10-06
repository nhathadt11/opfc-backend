using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Interfaces
{
    public interface IEventService
    {
        bool SaveEvent(Event newEvent);

        bool UpdateEvent(Event modifiedEvent);

        bool DeleteEvent(long eventId, long userId);

        Event GetEventById(long eventId);

        List<Event> GettAllEvent();
    }
}
