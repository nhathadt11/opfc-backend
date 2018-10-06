using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Interfaces
{
    public interface IEventTypeService
    {
        List<EventType> GetAllEventType();
    }
}
