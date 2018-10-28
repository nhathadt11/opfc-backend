using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    public interface IEventCategoryRepository: IRepository<EventCategory>
    {
        List<EventCategory> GetAllByEventId(long eventId);
    }
}
