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
    [Route("[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
<<<<<<< HEAD
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
=======
        public IActionResult Create(CreateMealRequest request)
        {
            try
            {
                var meal = Mapper.Map<Meal>(request.Meal);
                var created = Mapper.Map<MealDTO>(_serviceUow.MealService.CreateMeal(meal));

                return Created("/Meal", created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAll()
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        {
            try
            {
                var meals = _serviceUow.MealService.GetAllMeal();
                return Ok(Mapper.Map<List<MealDTO>>(meals));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

<<<<<<< HEAD
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

=======
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var found = _serviceUow.MealService.GetMealById(id);
                if (found == null)
                {
                    return NotFound("Meal could not be found.");
                }
                
                return Ok(Mapper.Map<MealDTO>(found));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateMealRequest request)
        {
            try
            {
                var found = _serviceUow.MealService.isExist(id);
                if (!found)
                {
                    return NotFound("Meal could not be found.");
                }

                var meal = Mapper.Map<Meal>(request.Meal);
                return Ok(Mapper.Map<MealDTO>(_serviceUow.MealService.UpdateMeal(meal)));
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
                var found = _serviceUow.MealService.isExist(id);
                if (!found)
                {
                    return NotFound("Meal could not be found.");
                }

                _serviceUow.MealService.DeleteMealById(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("Brand/{brandId}")]
        public IActionResult GetAllByBrandId(long brandId)
        {
            try
            {
                var foundBrand = _serviceUow.BrandService.GetBrandById(brandId);
                if (foundBrand == null)
                {
                    return NotFound("Brand could not be found.");
                }

                List<Meal> mealList = _serviceUow.MealService.GetAllByBrandId(brandId);
                return Ok(Mapper.Map<List<MealDTO>>(mealList));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }

        [HttpDelete]
        [Route("/Meal")]
        public ActionResult Delete(DeleteMealRequest request)
        {
            try
            {
                var meal = Mapper.Map<BrandDTO>(request.Meal);


                if (string.IsNullOrEmpty(meal.Id.ToString()) || !Regex.IsMatch((meal.Id.ToString()), "^\\d+$"))
                    return NotFound(new { Message = "Invalid Id" });


                var foundMeal = _serviceUow.MealService.GetMealById(meal.Id);
                if (foundMeal == null)
                {
                    return NotFound(new { Message = " could not find meal to delete" });
                }

                foundMeal.IsDeleted = true;

                try
                {
                    _serviceUow.MealService.UpdateMeal(foundMeal);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

    }
}