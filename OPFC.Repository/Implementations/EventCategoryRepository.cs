using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class EventCategoryRepository : EFRepository<EventCategory>, IEventCategoryRepository
    {
        public EventCategoryRepository(DbContext dbContext) : base(dbContext) { }

        public List<EventCategory> GetAllByEventId(long eventId)
        {
            return DbSet.Where(ec => ec.EventId == eventId).ToList();
        }
    }
}
