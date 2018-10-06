using AutoMapper;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Event;
using OPFC.API.Services.Interfaces;
using OPFC.Models;
using OPFC.Services.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Implementations
{
    public class EventAPIService : IEventAPIService
    {
        private IEventService _eventService = AppHostBase.Instance.Resolve<IEventService>();

        public CreateEventResponse Post(CreateEventRequest request)
        {
            try
            {
                var eventReq = Mapper.Map<EventDTO>(request.Event);

                var result = _eventService.SaveEvent(Mapper.Map<Event>(eventReq));

                return new CreateEventResponse
                {
                    Event = Mapper.Map<EventDTO>(result)
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UpdateEventResponse Post(UpdateEventRequest request)
        {
            try
            {
                var eventReq = Mapper.Map<EventDTO>(request.Event);

                var result = _eventService.UpdateEvent(Mapper.Map<Event>(eventReq));

                return new UpdateEventResponse
                {
                    Event = Mapper.Map<EventDTO>(result)
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DeleteEventResponse Post(DeleteEventRequest request)
        {
            try
            {
                var eventIdReq = request.EventId;
                var userIdReq = request.UserId;

                var result = _eventService.DeleteEvent(1,1);

                return new DeleteEventResponse
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
