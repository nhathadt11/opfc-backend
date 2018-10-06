using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class EventRepository : EFRepository<Event>, IEventRepository
    {
        public EventRepository(DbContext dbContext) : base(dbContext) { }

        public Event GetEventById(long eventId)
        {
            return DbSet.SingleOrDefault(x => x.Id == eventId && x.IsDeleted == false);
        }

        public List<Event> GettAllEvent()
        {
            return DbSet.Where(x => x.IsDeleted == false).ToList();
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
