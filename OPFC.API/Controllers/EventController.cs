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
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        public ActionResult Post(CreateEventRequest request)
        {
            try
            {
                var eventReq = Mapper.Map<EventDTO>(request.Event);
                var created = _serviceUow.EventService.SaveEvent(Mapper.Map<Event>(eventReq));

                return Created("/Event", Mapper.Map<EventDTO>(created));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var result = _serviceUow.EventService.GetAllEvent();
                return Ok(Mapper.Map<List<EventDTO>>(result));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(long id, UpdateEventRequest request)
        {
            try
            {
                var found = _serviceUow.EventService.GetEventById(id);
                if (found == null)
                {
                    return NotFound("Event could not be found.");
                }

                var eventReq = Mapper.Map<EventDTO>(request.Event);
                var result = _serviceUow.EventService.UpdateEvent(Mapper.Map<Event>(eventReq));
                return Ok(Mapper.Map<EventDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}/User/{userId}")]
        public ActionResult Post(long id, long userId)
        {
            try
            {
                var foundEvent = _serviceUow.EventService.GetEventById(id);
                if (foundEvent == null)
                {
                    return NotFound(new { Message = "Event could not be found." });
                }

                var foundUser = _serviceUow.UserService.GetUserById(userId);
                if (foundUser == null)
                {
                    return NotFound(new { Message = "User could not be found." });
                }

                _serviceUow.EventService.DeleteEvent(id, userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/Event/GetAllEventType")]
        public ActionResult GetAllEventType()
        {

            var eventTypes = Mapper.Map<List<EventTypeDTO>>(_serviceUow.EventTypeService.GetAllEventType());
            return Ok(eventTypes);
        }

        [HttpGet("User/{userId}")]
        public ActionResult<List<EventDTO>> GetAllByUserId(long userId)
        {
            try
            {
                var user = _serviceUow.UserService.GetUserById(userId);
                if (user == null)
                {
                    return NotFound("User could not be found.");
                }

                var eventList = _serviceUow.EventService.GetAllEventByUserId(userId);
                return Ok(Mapper.Map<List<EventDTO>>(eventList));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("/Event/MatchedEvent")]
        public ActionResult<List<EventDTO>> FindAllMatchedEvent(FindMatchedEventRequest request)
        {
            try
            {
                var result = _serviceUow.EventService.FindMatchedEvent(request.ServiceLocation, request.ServingNumber, request.Price, request.EventTypeIds);
                return Ok(Mapper.Map<List<EventDTO>>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}