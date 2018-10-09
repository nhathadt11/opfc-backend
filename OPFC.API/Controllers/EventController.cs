using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
        [Route("/Event")]
        public ActionResult Create(CreateEventRequest request)
        {
            try
            {
                var eventReq = Mapper.Map<EventDTO>(request.Event);

                var result = _serviceUow.EventService.SaveEvent(Mapper.Map<Event>(eventReq));

                return Created("/Event", Mapper.Map<EventDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        [Route("/Event")]
        public ActionResult GetAll()
        {
            var result = _serviceUow.EventService.GettAllEvent();

            return Ok(Mapper.Map<List<EventDTO>>(result));
        }

        [HttpPut]
        [Route("/Event")]
        public ActionResult Update(UpdateEventRequest request)
        {
            try
            {
                var eventReq = Mapper.Map<EventDTO>(request.Event);

                var result = _serviceUow.EventService.UpdateEvent(Mapper.Map<Event>(eventReq));

                return Ok(Mapper.Map<EventDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("/Event")]
        public ActionResult Delete(DeleteEventRequest request)
        {
            try
            {
                var eventIdReq = request.EventId;
                var userIdReq = request.UserId;

                var foundEvent = _serviceUow.EventService.GetEventById(eventIdReq);
                if (foundEvent == null)
                {
                    return NotFound(new { Message = "Could not find event" });
                }
                
                var foundUser = _serviceUow.UserService.GetUserById(userIdReq);
                if (foundUser == null)
                {
                    return NotFound(new {Message = "Could not find user"});
                }
                
                _serviceUow.EventService.DeleteEvent(eventIdReq, userIdReq);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        [Route("/Event/GetAllEventType")]
        public ActionResult GetAllEventType()
        {
            var userId = HttpContext.User?.Identity?.Name;

            var eventTypes = Mapper.Map<List<EventTypeDTO>>(_serviceUow.EventTypeService.GetAllEventType());
            return Ok(eventTypes);
        }
    }
}