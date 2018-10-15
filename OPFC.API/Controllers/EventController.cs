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
<<<<<<< HEAD
        [Route("/Event")]
        public ActionResult Create(CreateEventRequest request)
=======
        public ActionResult Post(CreateEventRequest request)
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        {
            try
            {
                var eventReq = Mapper.Map<EventDTO>(request.Event);
                var created = _serviceUow.EventService.SaveEvent(Mapper.Map<Event>(eventReq));

<<<<<<< HEAD
                var result = _serviceUow.EventService.SaveEvent(Mapper.Map<Event>(eventReq));

                return Created("/Event", Mapper.Map<EventDTO>(result));
=======
                return Created("/Event", Mapper.Map<EventDTO>(created));
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
<<<<<<< HEAD
        [Route("/Event")]
=======
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
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

<<<<<<< HEAD
        [HttpPut]
        [Route("/Event")]
        public ActionResult Update(UpdateEventRequest request)
=======
        [HttpPut("{id}")]
        public ActionResult Update(long id, UpdateEventRequest request)
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
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

<<<<<<< HEAD
        [HttpDelete]
        [Route("/Event")]
        public ActionResult Delete(DeleteEventRequest request)
=======
        [HttpDelete("{id}/User/{userId}")]
        public ActionResult Post(long id, long userId)
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
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
                    return NotFound(new {Message = "User could not be found."});
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
    }
}