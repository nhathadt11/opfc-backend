using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventTypeController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpGet]
        public ActionResult<List<EventTypeDTO>> GetAll()
        {
            try
            {
                var eventTypeList = _serviceUow.EventTypeService.GetAllEventType();
                return Ok(Mapper.Map<List<EventTypeDTO>>(eventTypeList));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}