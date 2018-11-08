using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
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
        
        [AllowAnonymous]
        [HttpGet("Limit")]
        public IActionResult GetLimit()
        {
            try
            {
                var menuList = _serviceUow.MenuService.GetAllMenu().Take(20);
                return Ok(Mapper.Map<List<MenuDTO>>(menuList));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
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

                var returnMenu = Mapper.Map<MenuDTO>(found);

                var mealList = _serviceUow.MealService.GetAllMealByMenuId(returnMenu.Id);
                returnMenu.MealList = mealList;

                var eventTypeList = _serviceUow.EventTypeService.GetAllEventTypeByMenuId(returnMenu.Id);
                returnMenu.EventTypeList = eventTypeList;

                var categoryList = _serviceUow.CategoryService.GetAllByMenuId(id);
                returnMenu.CategoryList = categoryList;

                var brand = _serviceUow.BrandService.GetBrandById(returnMenu.BrandId);
                returnMenu.BrandName = brand.BrandName;

                returnMenu.BrandPhone = brand.Phone;
                returnMenu.BrandParticipantNumber = brand.ParticipantNumber;
                returnMenu.BrandEmail = brand.Email;

                return Ok(returnMenu);
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
                var menu = Mapper.Map<Menu>(request);
                var created = _serviceUow.MenuService.CreateMenu(menu);
                return Created("/Menu", Mapper.Map<MenuDTO>(created));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Brand/{brandId}")]
        public IActionResult CreateMenuByBrand(long brandId, [FromBody] CreateMenuRequest request)
        {
            try
            {
                var created = _serviceUow.MenuService.CreateMenuByBrand(brandId, request);
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

                var updated = Mapper.Map<Menu>(request);
                return Ok(Mapper.Map<MenuDTO>(_serviceUow.MenuService.UpdateMenu(updated)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Brand/{brandId}/{menuId}")]
        public IActionResult Update(long brandId, long menuId, UpdateMenuRequest request)
        {
            try
            {
                var foundBrand = _serviceUow.BrandService.GetBrandById(brandId);
                if (foundBrand == null)
                {
                    return NotFound("Brand could not be found.");
                }

                var foundMenu = _serviceUow.MenuService.GetMenuById(menuId);
                if (foundMenu == null)
                {
                    return NotFound("Menu could not be found.");
                }

                return Ok(Mapper.Map<MenuDTO>(_serviceUow.MenuService.UpdateMenuByBrand(brandId, menuId, request)));
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

        [AllowAnonymous]
        [HttpGet("Brand/{brandId}")]
        public ActionResult GetAllMenuByBrandId(long brandId)
        {
            try
            {
                var foundMenuList = _serviceUow.MenuService.GetAllMenuByBrandId(brandId);
                var returnMenuList = Mapper.Map<List<MenuDTO>>(foundMenuList);
                foreach (var menu in returnMenuList)
                {
                    var mealList = _serviceUow.MealService.GetAllMealByMenuId(menu.Id);
                    menu.MealIds = mealList.Select(m => m.Id).ToList();
                    menu.MealNames = mealList.Select(m => m.MealName).ToList();

                    var eventTypeList = _serviceUow.EventTypeService.GetAllEventTypeByMenuId(menu.Id);
                    menu.EventTypeIds = eventTypeList.Select(e => e.Id).ToList();
                    menu.EventTypeNames = eventTypeList.Select(e => e.EventTypeName).ToList();

                    var categoryList = _serviceUow.CategoryService.GetAllByMenuId(menu.Id);
                    menu.CategoryIds = categoryList.Select(c => c.Id).ToList();
                    menu.CategoryNames = categoryList.Select(c => c.Name).ToList();
                }
                
                return Ok(returnMenuList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}