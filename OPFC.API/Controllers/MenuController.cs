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
                returnMenu.Photo = found.Photo?.Split(";").ToArray();
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

                var updated = _serviceUow.MenuService.UpdateMenu(Mapper.Map<Menu>(request));
                var result = Mapper.Map<MenuDTO>(updated);
                result.Photo = updated.Photo?.Split(";").ToArray();

                return Ok(result);
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
                //var foundBrand = _serviceUow.BrandService.GetBrandById(brandId);
                var isBrandExist = _serviceUow.BrandService.Exists(brandId);
                if (!isBrandExist)
                {
                    return NotFound("Brand could not be found.");
                }

                //var foundMenu = _serviceUow.MenuService.GetMenuById(menuId);
                var isMenuExist = _serviceUow.MenuService.Exists(menuId);
                if (!isMenuExist)
                {
                    return NotFound("Menu could not be found.");
                }
                var updated = _serviceUow.MenuService.UpdateMenuByBrand(brandId, menuId, request);
                var result = Mapper.Map<MenuDTO>(updated);
                result.Photo = updated.Photo?.Split(";");

                return Ok(result);
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
                var isExist = _serviceUow.MenuService.Exists(id);
                if (!isExist)
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
        public ActionResult GetAllMenuByBrandId(long brandId, int page = 1, int size = 12)
        {
            try
            {
                var foundMenuList = _serviceUow.MenuService.GetAllMenuByBrandId(brandId);
                var returnMenuList = Mapper.Map<List<MenuDTO>>(foundMenuList);

                returnMenuList.ForEach(x =>
                {
                    x.Photo = foundMenuList.SingleOrDefault(y => y.Id == x.Id).Photo?.Split(";");
                });

                foreach (var menu in returnMenuList)
                {
                    var menuIndex = returnMenuList.IndexOf(menu);
                    var tempMenu = foundMenuList[menuIndex];

                    //var mealList = _serviceUow.MealService.GetAllMealByMenuId(menu.Id);
                    if (tempMenu.MenuMealList.Any() && tempMenu.MenuMealList != null)
                    {
                        menu.MealIds = tempMenu.MenuMealList.Select(m => m.Meal.Id).ToList();
                        menu.MealNames = tempMenu.MenuMealList.Select(m => m.Meal.MealName).ToList();
                    }

                    //var eventTypeList = _serviceUow.EventTypeService.GetAllEventTypeByMenuId(menu.Id);
                    if (tempMenu.MenuEventTypeList.Any() && tempMenu.MenuEventTypeList != null)
                    {
                        menu.EventTypeIds = tempMenu.MenuEventTypeList.Select(e => e.EventType).Select(e => e.Id).ToList();
                        menu.EventTypeNames = tempMenu.MenuEventTypeList.Select(e => e.EventType).Select(e => e.EventTypeName).ToList();
                    }

                    //var categoryList = _serviceUow.CategoryService.GetAllByMenuId(menu.Id);

                    if (tempMenu.MenuCategoryList.Any() && tempMenu.MenuCategoryList != null)
                    {
                        menu.CategoryIds = tempMenu.MenuCategoryList.Select(c => c.Category).Select(c => c.Id).ToList();
                        menu.CategoryNames = tempMenu.MenuCategoryList.Select(c => c.Category).Select(c => c.Name).ToList();
                    }
                }

                var pagedMenuList = returnMenuList.Skip((page - 1) * size).Take(size);
                var total = returnMenuList.Count;

                return Ok(new { menuList = pagedMenuList, total });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("User/{userId}/Bookmark")]
        public ActionResult GetAllByUserId(long userId)
        {
            try
            {
                var userExists = _serviceUow.UserService.IsUserExist(userId);
                if (!userExists)
                {
                    return NotFound("User does not exist.");
                }

                var result = _serviceUow.MenuService.GetAllBookmarkedMenuByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("User/{userId}/Bookmark/MenuIds")]
        public ActionResult GetAllBookmarkedMenuIdsByUserId(long userId)
        {
            try
            {
                var userExists = _serviceUow.UserService.IsUserExist(userId);
                if (!userExists)
                {
                    return NotFound("User does not exists.");
                }

                var result = _serviceUow.MenuService.GetAllBookmarkedMenuIdsByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}