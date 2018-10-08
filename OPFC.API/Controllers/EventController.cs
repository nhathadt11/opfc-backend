using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Event;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
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

        [HttpGet]
        [Route("/Event/GetAllEvent/")]
        public GetAllEventResponse GetAllEvent()
        {
            try
            {
                return new GetAllEventResponse
                {
                    Events = Mapper.Map<List<EventDTO>>(_serviceUow.EventService.GettAllEvent()),
                    ResponseStatus = new ServiceStack.ResponseStatus("200 OK")
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPut]
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

        [HttpDelete]
        [Route("/Event/DeleteEvent/")]
        public DeleteEventResponse Post(DeleteEventRequest request)
        {
            try
            {
                var eventIdReq = request.EventId;
                var userIdReq = HttpContext.User?.Identity?.Name;

                var result = _serviceUow.EventService.DeleteEvent(eventIdReq, Int64.Parse(userIdReq));

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

        [HttpGet]
        [Route("/Event/GetAllEventType/")]
        public GetAllEventTypeResponse GetAllEventType()
        {
            var userId = HttpContext.User?.Identity?.Name;

            try
            {
                return new GetAllEventTypeResponse
                {
                    Event = Mapper.Map<List<EventTypeDTO>>(_serviceUow.EventTypeService.GetAllEventType()),
                    ResponseStatus = new ServiceStack.ResponseStatus("200 OK")
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}