using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Meal;
using OPFC.Models;
using OPFC.Services.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        [Route("/Meal")]
        public ActionResult Create(CreateMealRequest request)
        {
            try
            {
                var meal = Mapper.Map<MealDTO>(request.Meal);

                var result = _serviceUow.MealService.CreateMeal(Mapper.Map<Meal>(meal));

                return Created("/Meal", Mapper.Map<MealDTO>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }

        [HttpGet]
        [Route("/Meal")]
        public ActionResult GetAll()
        {
            var meals = _serviceUow.MealService.GetAllMeal();

            return Ok(Mapper.Map<List<MealDTO>>(meals));

        }

        [HttpGet]
        [Route("/Meal/{id}")]
        public ActionResult Get(string id)
        {

            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            var meal = _serviceUow.MealService.GetMealById(long.Parse(id));
            if(meal == null)
            {
                return NotFound(new { Message = "Could not find Meal" });
            }
            return Ok(Mapper.Map<MealDTO>(meal));
        }

        [HttpPut]
        [Route("/Meal")]
        public ActionResult Update(UpdateMealRequest request)
        {
            try
            {
                var meal = Mapper.Map<MealDTO>(request.Meal);

                var result = _serviceUow.MealService.UpdateMeal(Mapper.Map<Meal>(meal));

                return Ok(Mapper.Map<MealDTO>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }
    }
}