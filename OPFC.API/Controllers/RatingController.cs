using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Rating;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Route("[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var ratingList = _serviceUow.RatingService.GetAllRating();
                return Ok(Mapper.Map<List<RatingDTO>>(ratingList));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var found = _serviceUow.RatingService.GetRatingById(id);
                if (found == null)
                {
                    return NotFound("Rating could not be found.");
                }
                return Ok(Mapper.Map<RatingDTO>(found));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("Menu/{id}")]
        public IActionResult GetByMenuId(long id)
        {
            try
            {
                if (!_serviceUow.MenuService.Exists(id)) throw new Exception("Menu could not be found.");

                var retrieved = _serviceUow.RatingService.GetAllRating().Where(r => r.MenuId == id);
                var returnRatingList = Mapper.Map<List<RatingDTO>>(retrieved);
                
                returnRatingList.ForEach(r =>
                {
                    r.Author = _serviceUow.UserService.GetUserById(r.UserId).Username;
                    r.CityName = _serviceUow.UserService.GetCityNameForUserId(r.UserId);
                });
                return Ok(returnRatingList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Menu/{menuId}/User/{userId}")]
        public IActionResult Create(long menuId, long userId, CreateRatingRequest request)
        {
            try
            {
                request.MenuId = menuId;
                request.UserId = userId;
                var rating = Mapper.Map<Rating>(request);
                var created = Mapper.Map<RatingDTO>(_serviceUow.RatingService.CreateRating(rating));

                return Created("Rating", created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateRatingRequest request)
        {
            try
            {
                var found = _serviceUow.RatingService.GetRatingById(id);
                if (found == null)
                {
                    return NotFound("Rating could not be found.");
                }

                var rating = Mapper.Map<Rating>(request.Rating);
                return Ok(Mapper.Map<RatingDTO>(_serviceUow.RatingService.UpdateRating(rating)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var found = _serviceUow.RatingService.GetRatingById(id);
                if (found == null)
                {
                    return NotFound("Rating could not be found.");
                }

                _serviceUow.RatingService.DeleteRatingById(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}




