using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ServiceLocationController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpGet("/ServiceLocation/Brand/{brandId}")]
        public ActionResult<List<ServiceLocation>> GetServiceLocationByBrandId(long brandId)
        {
            try
            {
                var result = _serviceUow.ServiceLocationService
                    .GetServiceLocationsByBrandId(brandId)
                    .Select(s => s.DistrictId)
                    .ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/ServiceLocation/Brand/{brandId}")]
        public ActionResult UpdateServiceLocationByBrandId(long brandId, [FromBody]List<long> serviceLocationIds)
        {
            try
            {
                _serviceUow
                    .ServiceLocationService
                    .UpdateServiceLocationByBrand(brandId, serviceLocationIds);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}