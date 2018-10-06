using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class EventTypeRepository : EFRepository<EventType>, IEventTypeRepository
    {
        public EventTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<EventType> GetAllEventType()
        {
            return DbSet.ToList();
        }
    }
}
