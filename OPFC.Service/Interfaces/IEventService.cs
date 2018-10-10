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

        List<Event> GettAllEvent();
    }
}
