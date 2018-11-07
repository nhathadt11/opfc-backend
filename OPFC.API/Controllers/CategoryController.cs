using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        readonly IServiceUow _serviceUow = ServiceStack.ServiceStackHost.Instance.TryResolve<IServiceUow>();

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<CategoryDTO>> GetAll()
        {
            try
            {
                var categoryList = _serviceUow.CategoryService.GetAll();
                return Ok(Mapper.Map<List<CategoryDTO>>(categoryList));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
