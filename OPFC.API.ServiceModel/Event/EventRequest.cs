using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.Event
{
    public class CreateEventRequest : IReturn<CreateEventResponse>
    {
        public EventDTO Event { get; set; }
    }

    public class UpdateEventRequest : IReturn<UpdateEventResponse>
    {
        public EventDTO Event { get; set; }
    }

    public class DeleteEventRequest : IReturn<DeleteEventResponse>
    {
        public long EventId { get; set; }

        public long UserId { get; set; }
    }

    public class GetAllEventRequest : IReturn<GetAllEventResponse>
    {
    }

    public class GetAllEventTypeRequest : IReturn<GetAllEventTypeResponse>
    {
    }
}
