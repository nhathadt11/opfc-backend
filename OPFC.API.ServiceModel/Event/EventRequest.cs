using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.Event
{
    [EnableCors("*", "*")]
    [Route("/Event/CreateEvent/", "POST")]
    public class CreateEventRequest : IReturn<CreateEventResponse>
    {
        public EventDTO Event { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Event/UpdateEvent/", "POST")]
    public class UpdateEventRequest : IReturn<UpdateEventResponse>
    {
        public EventDTO Event { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Event/DeleteEvent/", "POST")]
    public class DeleteEventRequest : IReturn<DeleteEventResponse>
    {
        public long EventId { get; set; }

        public long UserId { get; set; }
    }
}
