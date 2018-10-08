using System;
using System.Collections.Generic;
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
        [Route("/Rating/CreateRating/")]
        public CreateRatingResponse Post(CreateRatingRequest request)
        {
            var rating = Mapper.Map<Rating>(request);

            return new CreateRatingResponse
            {
                Rating = Mapper.Map<RatingDTO>(_serviceUow.RatingService.CreateRating(rating))
            };

        }

        [HttpGet]
        [Route("/Rating/GetRatingById/{id}")]
        public GetRatingByIdResponse Get(GetRatingByIdRequest request)
        {
            var rating = _serviceUow.RatingService.GetRatingById(request.Id);
            return new GetRatingByIdResponse
            {
                Rating = Mapper.Map<RatingDTO>(rating)
            };
        }

        [HttpPut]
        [Route("/Rating/UpdateRating/")]
        public UpdateRatingResponse Post(UpdateRatingRequest request)
        {
            var rating = Mapper.Map<Rating>(request.Rating);

            return new UpdateRatingResponse
            {
                Rating = Mapper.Map<RatingDTO>(_serviceUow.RatingService.UpdateRating(rating))
            };
        }

        [HttpGet]
        [Route("/Rating/GetAllRating/")]
        public GetAllRatingResponse Get(GetAllRatingRequest request)
        {
            var ratingList = _serviceUow.RatingService.GetAllRating();

            return new GetAllRatingResponse
            {
                Ratings = Mapper.Map<List<RatingDTO>>(ratingList)
            };

        }

    }
}




