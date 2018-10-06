using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Event;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Route("/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [Route("/Event/CreateEvent/")]
        public CreateEventResponse Post(CreateEventRequest request)
        {
            try
            {
                var eventReq = Mapper.Map<EventDTO>(request.Event);

                var result = _serviceUow.EventService.SaveEvent(Mapper.Map<Event>(eventReq));

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

        [Route("/Event/UpdateEvent/")]
        public UpdateEventResponse Post(UpdateEventRequest request)
        {
            try
            {
                var eventReq = Mapper.Map<EventDTO>(request.Event);

                var result = _serviceUow.EventService.UpdateEvent(Mapper.Map<Event>(eventReq));

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

        [Route("/Event/DeleteEvent/")]
        public DeleteEventResponse Post(DeleteEventRequest request)
        {
            try
            {
                var eventIdReq = request.EventId;
                var userIdReq = request.UserId;

                var result = _serviceUow.EventService.DeleteEvent(1, 1);

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