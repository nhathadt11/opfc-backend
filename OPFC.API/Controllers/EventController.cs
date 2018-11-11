using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Event;
using OPFC.Models;
using OPFC.Recommendation;
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
        
        [HttpGet("{eventId}")]
        public ActionResult Get(long eventId)
        {
            try
            {
                var foundEvent = _serviceUow.EventService.GetEventById(eventId);
                if (foundEvent == null)
                {
                    return NotFound("Event could not be found");
                }

                return Ok(foundEvent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("User/{userId}")]
        public ActionResult Post(long userId, CreateEventRequest request)
        {
            try
            {
                var foundUser = _serviceUow.UserService.GetUserById(userId);
                if (foundUser == null)
                {
                    return NotFound("User could not be found.");
                }

                var eventReq = Mapper.Map<EventDTO>(request.Event);
                eventReq.UserId = userId;

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
        public ActionResult Update(UpdateEventRequest request)
        {
            try
            {
                var id = request.Event.Id;
                var found = _serviceUow.EventService.IsEventExist(id);
                if (!found)
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

        [HttpPut("User/{userId}/{id}")]
        public ActionResult Update(long userId, long id, UpdateEventRequest request)
        {
            try
            {
                var foundUser = _serviceUow.UserService.GetUserById(userId);
                if (foundUser == null)
                {
                    return NotFound("User could not be found.");
                }

                var eventExits = _serviceUow.EventService.IsEventExist(id);
                if (!eventExits)
                {
                    return NotFound("Event could not be found.");
                }
                var toBeUpdate = Mapper.Map<Event>(request.Event);

                var result = _serviceUow.EventService.UpdateEvent(toBeUpdate);
                return Ok(Mapper.Map<EventDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("User/{userId}/{id}")]
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

        #region Recommendation system code
        //[AllowAnonymous]
        //[HttpGet("/User/{userId}/Event/{eventId}/GetSuggestion")]
        //public ActionResult<List<object>> GetSuggestion(long userId, long eventId)
        //{
        //    try
        //    {

        //        Class1 class1 = new Class1();
        //        Recommendation.Objects.UserBehavior userBehavior = new Recommendation.Objects.UserBehavior();
        //        userBehavior.Users = _serviceUow.UserService.GetAllUser();
        //        userBehavior.Menus = _serviceUow.MenuService.GetAllMenu();
        //        userBehavior.Categories = _serviceUow.CategoryService.GetAll();

        //        foreach (var user in userBehavior.Users)
        //        {
        //            foreach (var menu in userBehavior.Menus)
        //            {
        //                userBehavior.UserActions.Add(new Recommendation.Objects.UserAction(user.Id, "", menu.Id, menu.MenuName));
        //            }
        //        }

        //        var result = class1.GetSuggest(userBehavior, userId);

        //        var suggestedMenuIds = result.Select(x => x.MenuId).Distinct().ToList();

        //        var rs = _serviceUow.EventService.GetSuggestion(eventId, suggestedMenuIds);

        //        return rs;
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        #endregion

        [HttpGet("/Event/GetSuggestion/{eventId}")]
        public ActionResult<List<List<Menu>>> GetSuggestion(long eventId, int? page, int? size)
        {
            var takePage = page ?? 1;
            var takeSize = size ?? 10;

            try
            {
                var combos = _serviceUow.EventService.GetSuggestion(eventId);
                var total = combos.Count;
                var result = combos
                    .Skip((takePage - 1) * takeSize)
                    .Take(takeSize)
                    .ToList();

                return Ok(new { total, result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}