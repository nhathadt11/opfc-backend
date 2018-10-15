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

<<<<<<< HEAD
        [HttpPost]
        [Route("/Rating")]
        public ActionResult Create(CreateRatingRequest request)
        {
            try
            {
                var rating = Mapper.Map<MealDTO>(request.Rating);

                var result = _serviceUow.RatingService.CreateRating(Mapper.Map<Rating>(rating));

                return Created("/Rating", Mapper.Map<RatingDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }

        [HttpGet]
        [Route("/Rating")]
        public ActionResult GetAll()
        {
            var ratings = _serviceUow.RatingService.GetAllRating();

            return Ok(Mapper.Map<List<RatingDTO>>(ratings));

        }

        [HttpGet]
        [Route("/Rating/{id}")]
        public ActionResult Get(string id)
        {

            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            var rating = _serviceUow.RatingService.GetRatingById(long.Parse(id));
            if (rating == null)
            {
                return NotFound(new { Message = "Could not find rating" });
            }
            return Ok(Mapper.Map<RatingDTO>(rating));
        }

        [HttpPut]
        [Route("/Rating")]
        public ActionResult Update(UpdateRatingRequest request)
        {
            try
            {
                var rating = Mapper.Map<RatingDTO>(request.Rating);

                var result = _serviceUow.RatingService.UpdateRating(Mapper.Map<Rating>(rating));

                return Ok(Mapper.Map<RatingDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
=======
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
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298

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




