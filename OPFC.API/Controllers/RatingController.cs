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
    [Route("/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

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

        }

    }
}




