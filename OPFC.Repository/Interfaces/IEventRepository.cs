using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    public interface IEventRepository
    {
        void SaveEvent(Event newEvent);

        void UpdateEvent(Event modifiedEvent);

        Event GetEventById(long eventId);

        List<Event> GettAllEvent();
    }
}
