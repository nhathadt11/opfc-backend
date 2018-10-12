using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var menuList = _serviceUow.MenuService.GetAllMenu();
                return Ok(Mapper.Map<List<MenuDTO>>(menuList));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var found = _serviceUow.MenuService.GetMenuById(id);
                if (found == null)
                {
                    return NotFound("Menu could not be found.");
                }
                return Ok(Mapper.Map<MenuDTO>(found));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(CreateMenuRequest request)
        {
            try
            {
                var menu = Mapper.Map<Menu>(request.Menu);
                var created = _serviceUow.MenuService.CreateMenu(menu);
                return Created("/Menu", Mapper.Map<MenuDTO>(created));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateMenuRequest request)
        {
            try
            {
                var found = _serviceUow.MenuService.GetMenuById(id);
                if (found == null)
                {
                    return NotFound("Menu could not be found.");
                }

                var updated = Mapper.Map<Menu>(request.Menu);
                return Ok(Mapper.Map<MenuDTO>(_serviceUow.MenuService.UpdateMenu(updated)));
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
                var found = _serviceUow.MenuService.GetMenuById(id);
                if (found == null)
                {
                    return NotFound("Menu could not be found.");
                }

               _serviceUow.MenuService.DeleteMenuById(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Brand/{brandId}")]
        public ActionResult GetAllMenuByBrandId(long brandId)
        {
            try
            {
                var menuList = _serviceUow.MenuService.GetAllMenuByBrandId(brandId);
                foreach (var menu in menuList)
                {
                    var mealList = _serviceUow.MealService.GetAllMealByMenuId(menu.Id);
                    menu.MealList = mealList;
                }

                return Ok(menuList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}