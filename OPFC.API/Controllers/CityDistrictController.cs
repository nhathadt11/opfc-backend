using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    public class CityDistrictController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

<<<<<<< HEAD
        [HttpGet]
        [Route("/District/{id}")]
=======
        [HttpGet("/District/{id}")]
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        public ActionResult GetDistrictById(string id)
        {

            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            try
            {
                var district = _serviceUow.DistrictService.GetDistrictById(long.Parse(id));
                if (district == null)
                {
                    return NotFound(new { Message = "not found District" });
                }

                return Ok(Mapper.Map<DistrictDTO>(district));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

<<<<<<< HEAD
        [HttpGet]
        [Route("/District")]
=======
        [HttpGet("/District")]
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        public ActionResult GetAllDistrict()
        {
            var districts = _serviceUow.DistrictService.GetAllDistrict();

            return Ok(Mapper.Map<List<DistrictDTO>>(districts));
        }

<<<<<<< HEAD
        [HttpGet]
        [Route("/City/{id}")]
=======
        [HttpGet("/City/{id}")]
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        public ActionResult GetCityById(string id)
        {
            
            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            try
            {
                var city = _serviceUow.CityService.GetCityById(long.Parse(id));

                return Ok(Mapper.Map<CityDTO>(city));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

<<<<<<< HEAD
        [HttpGet]
        [Route("/District/")]
=======
        [HttpGet("City")]
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        public ActionResult GetAllCity()
        {
            var cities = _serviceUow.CityService.GetAllCity();

            return Ok(Mapper.Map<List<CityDTO>>(cities));
        }

    }
}
