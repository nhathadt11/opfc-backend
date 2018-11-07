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
                var result = _serviceUow.ServiceLocationService.GetServiceLocationsByBrandId(brandId);
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}