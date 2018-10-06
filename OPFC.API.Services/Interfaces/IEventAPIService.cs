using OPFC.API.ServiceModel.Event;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Interfaces
{
    public interface IEventAPIService
    {
        CreateEventResponse Post(CreateEventRequest request);

        UpdateEventResponse Post(UpdateEventRequest request);

        DeleteEventResponse Post(DeleteEventRequest request);
    }
}
