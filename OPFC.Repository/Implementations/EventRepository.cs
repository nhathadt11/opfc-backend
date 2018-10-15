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

        public Event SaveEvent(Event newEvent)
        {
            return DbSet.Add(newEvent).Entity;
        }

        public Event UpdateEvent(Event modifiedEvent)
        {
            return DbSet.Update(modifiedEvent).Entity;
        }

        public List<Event> FindMatchedEvent(long serviceLocation, int servingNumber, decimal price, long[] eventTypeIds)
        {
            var availableEvents = DbSet.Where(e => e.IsDeleted == false && e.DistrictId == serviceLocation)
                                        .Include(e => e.District)
                                        .Include(e => e.City)
                                        .ToList();

            var result = availableEvents.Where(e => e.ServingNumber >= servingNumber &&
                                               e.Budget <= price && 
                                               eventTypeIds.Contains(e.EventTypeId))
                                        .ToList();

            return result;
        }
    }
}
