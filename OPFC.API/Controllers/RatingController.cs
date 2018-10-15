using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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

        [HttpPost]
        public IActionResult Create(CreateRatingRequest request)
        {
            try
            {
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




