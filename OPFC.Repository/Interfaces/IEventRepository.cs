using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Event SaveEvent(Event newEvent);

        Event UpdateEvent(Event modifiedEvent);

        Event GetEventById(long eventId);

        List<Event> GettAllEvent();

        List<Event> FindMatchedEvent(long serviceLocation, int servingNumber, decimal price, long[] eventTypeIds);
    }
}
