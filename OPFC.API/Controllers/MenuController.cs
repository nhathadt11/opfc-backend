using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Menu;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpGet]
        [Route("/Menu/{id}")]
        public ActionResult GetById(string id)
        {
            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            var menu = _serviceUow.MenuService.GetMenuById(long.Parse(id));
            if (menu == null)
                return NotFound(new { Message = "could not find menu" });

            return Ok(Mapper.Map<MenuDTO>(menu));

        }

        [HttpPost]
        [Route("/Menu")]
        public ActionResult Create(CreateMenuRequest request)
        {
            try
            {
                var menu = Mapper.Map<MenuDTO>(request.Menu);

                var result = _serviceUow.MenuService.CreateMenu(Mapper.Map<Menu>(menu));

                return Created("/Menu", Mapper.Map<Menu>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut]
        [Route("/Menu")]
        public ActionResult update(UpdateMenuRequest request)
        {
            try
            {
                var menu = Mapper.Map<MenuDTO>(request.Menu);

                var result = _serviceUow.MenuService.UpdateMenu(Mapper.Map<Menu>(menu));

                return Ok(Mapper.Map<MenuDTO>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        [Route("/Menu")]
        public ActionResult GetAll()
        {
            var result = _serviceUow.MenuService.GetAllMenu();

            return Ok(Mapper.Map<List<MealDTO>>(result));

        }

        [HttpDelete]
        [Route("/Meal")]
        public ActionResult Delete(DeleteMenuRequest request)
        {
            try
            {
                var menu = Mapper.Map<MenuDTO>(request.Menu);


                if (string.IsNullOrEmpty(menu.Id.ToString()) || !Regex.IsMatch((menu.Id.ToString()), "^\\d+$"))
                    return NotFound(new { Message = "Invalid Id" });


                var foundMenu = _serviceUow.MenuService.GetMenuById(menu.Id);
                if (foundMenu == null)
                {
                    return NotFound(new { Message = " could not find menu to delete" });
                }

                foundMenu.IsDeleted = true;

                try
                {
                    _serviceUow.MenuService.UpdateMenu(foundMenu);
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