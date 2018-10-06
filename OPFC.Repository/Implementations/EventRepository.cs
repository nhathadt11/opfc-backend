using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class EventRepository : EFRepository<Event>, IEventRepository
    {
        public EventRepository(DbContext dbContext) : base(dbContext) { }

        public void DeleteEvent(Event deleteEvent)
        {
            
        }

        public Event GetEventById(long eventId)
        {
            return DbSet.SingleAsync<Event>(x => x.Id == eventId).Result;
        }

        public List<Event> GettAllEvent()
        {
            return DbSet.ToListAsync<Event>().Result;
        }

        public void SaveEvent(Event newEvent)
        {
            DbSet.Add(newEvent);
        }

        public void UpdateEvent(Event modifiedEvent)
        {
            DbSet.Update(modifiedEvent);
        }
    }
}
