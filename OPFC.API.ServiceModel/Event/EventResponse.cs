using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.Event
{
    public class CreateEventResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public EventDTO Event { get; set; }
    }

    public class UpdateEventResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public EventDTO Event { get; set; }
    }

    public class DeleteEventResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
        public bool IsSuccess { get; set; }
    }
}
