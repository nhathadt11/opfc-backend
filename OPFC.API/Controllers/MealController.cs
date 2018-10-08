using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Meal;
using OPFC.Models;
using OPFC.Services.UnitOfWork;
using System;
using System.Collections.Generic;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        [Route("/Meal/CreatMeal/")]
        public CreateMealResponse Post(CreateMealRequest request)
        {
            var meal = Mapper.Map<Meal>(request.Meal);

            return new CreateMealResponse
            {
                Meal = Mapper.Map<MealDTO>(_serviceUow.MealService.CreateMeal(meal))
            };
        }

        [HttpGet]
        [Route("/Meal/GetAllMeal/")]
        public GetAllMealResponse Get()
        {
            var meals = _serviceUow.MealService.GetAllMeal();

            return new GetAllMealResponse
            {
                Meals = Mapper.Map<List<MealDTO>>(meals)
            };
        }

        [HttpGet]
        [Route("/Meal/GetMealById/{id}")]
        public GetMealByIdResponse Get(GetMealByIdRequest request)
        {
            var meal = _serviceUow.MealService.GetMealById(request.Id);

            return new GetMealByIdResponse
            {
                Meal = Mapper.Map<MealDTO>(meal)
            };
        }

        [HttpPut]
        [Route("/Meal/UpdateMeal/")]
        public UpdateMealResponse Post(UpdateMealRequest request)
        {
            var meal = Mapper.Map<Meal>(request.Meal);

            return new UpdateMealResponse
            {
                Meal = Mapper.Map<MealDTO>(_serviceUow.MealService.UpdateMeal(meal))
            };
        }
    }
}