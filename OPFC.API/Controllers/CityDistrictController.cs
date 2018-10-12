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

        [HttpGet]
        [Route("/District/{id}")]
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

        [HttpGet]
        [Route("/District")]
        public ActionResult GetAllDistrict()
        {
            var districts = _serviceUow.DistrictService.GetAllDistrict();

            return Ok(Mapper.Map<List<DistrictDTO>>(districts));
        }

        [HttpGet]
        [Route("/City/{id}")]
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

        [HttpGet]
        [Route("/District/")]
        public ActionResult GetAllCity()
        {
            var cities = _serviceUow.CityService.GetAllCity();

            return Ok(Mapper.Map<List<CityDTO>>(cities));
        }

    }
}
