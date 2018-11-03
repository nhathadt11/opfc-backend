using System;
using System.Collections;
using OPFC.Models;
using OPFC.Services.Interfaces;
using OPFC.Repositories.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using OPFC.API.ServiceModel.Menu;

namespace OPFC.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IOpfcUow _opfcUow;

        public MenuService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public Menu CreateMenu(Menu menu)
        {
            var result = _opfcUow.MenuRepository.CreateMenu(menu);
            _opfcUow.Commit();
            return result;
        }

        public List<Menu> GetAllMenu()
        {
            var menus = _opfcUow.MenuRepository.GetAllMenu();
            var menuCategories = _opfcUow.MenuCategoryRepository.GetAllByMenuIds(menus.Select(m => m.Id).ToList()).GroupBy(x => x.MenuId).ToList();
            var categories = _opfcUow.CategoryRepository.GetAll();
            foreach (var menu in menus)
            {
                var categoryIds = menuCategories.Where(x => x.Key == menu.Id)
                    .Select(x => x.Select(c => c.CategoryId).ToList()).ToList();

                if (categoryIds.Count > 0)
                {
                    menu.CategoryList = categories.Where(c => categoryIds.SingleOrDefault().Contains(c.Id)).ToList();
                }
                else
                {
                    menu.CategoryList = new List<Category>();
                }

            }
            return menus;
        }

        public void DeleteMenuById(long id)
        {
            var found = GetMenuById(id);
            if (found == null)
            {
                throw new Exception("Menu could not be found.");
            }

            found.IsDeleted = true;
            if (UpdateMenu(found) == null)
            {
                throw new Exception("Menu could not be updated.");
            }
        }

        public List<Menu> GetAllMenuByBrandId(long id)
        {
            return _opfcUow.MenuRepository.GetAllByBrandId(id);
        }

        public Menu CreateMenuByBrand(long brandId, CreateMenuRequest request)
        {
            var menu = new Menu
            {
                MenuName = request.MenuName,
                Description = request.Description,
                Price = request.Price,
                ServingNumber = request.ServingNumber,
                BrandId = brandId
            };

            var createdMenu = _opfcUow.MenuRepository.CreateMenu(menu);

            var mealIds = request.MealIds;
            var menuMealList = _opfcUow.MealRepository
                .GetAllMeal()
                .Where(m => mealIds.Contains(m.Id))
                .Select(m => new MenuMeal
                {
                    MenuId = menu.Id,
                    MealId = m.Id,
                    IsDeleted = false
                })
                .ToList();

            var eventTypeIds = request.EventTypeIds;
            var menuEventTypeList = _opfcUow.EventTypeRepository
                .GetAllEventType()
                .Where(e => eventTypeIds.Contains(e.Id))
                .Select(e => new MenuEventType
                {
                    MenuId = menu.Id,
                    EventTypeId = e.Id,
                })
                .ToList();

            _opfcUow.MenuMealRepository.CreateRange(menuMealList);
            _opfcUow.MenuEventTypeRepository.CreateRange(menuEventTypeList);
            _opfcUow.Commit();

            return createdMenu;
        }

        public Menu UpdateMenuByBrand(long brandId, long menuId, UpdateMenuRequest request)
        {
            var menuToUpdate = _opfcUow.MenuRepository.GetMenuById(menuId);
            menuToUpdate.MenuName = request.MenuName;
            menuToUpdate.Description = request.Description;
            menuToUpdate.Price = request.Price;
            menuToUpdate.ServingNumber = request.ServingNumber;

            var updated = _opfcUow.MenuRepository.UpdateMenu(menuToUpdate);

            // MenuMeal
            var oldMenuMealList = _opfcUow.MenuMealRepository
                .GetAll()
                .Where(mm => mm.MenuId == updated.Id)
                .ToList();
            _opfcUow.MenuMealRepository.RemoveRange(oldMenuMealList);

            var mealIds = request.MealIds;
            var newMenuMealList = mealIds.Select(id => new MenuMeal { MealId = id, MenuId = updated.Id }).ToList();
            _opfcUow.MenuMealRepository.CreateRange(newMenuMealList);

            //MenuEventType
            var oldMenuEventTypeList = _opfcUow.MenuEventTypeRepository
                .GetAll()
                .Where(mm => mm.MenuId == updated.Id)
                .ToList();
            _opfcUow.MenuEventTypeRepository.RemoveRange(oldMenuEventTypeList);

            var eventTypeIds = request.EventTypeIds;
            var newMenuEventTypeList = eventTypeIds.Select(id => new MenuEventType { EventTypeId = id, MenuId = updated.Id }).ToList();
            _opfcUow.MenuEventTypeRepository.CreateRange(newMenuEventTypeList);

            _opfcUow.Commit();
            return updated;
        }

        public bool Exists(long id)
        {
            return _opfcUow.MenuRepository.GetMenuById(id) != null;
        }

        public Menu GetMenuById(long id)
        {
            return _opfcUow.MenuRepository.GetMenuById(id);
        }

        public Menu UpdateMenu(Menu menu)
        {
            var result = _opfcUow.MenuRepository.UpdateMenu(menu);
            _opfcUow.Commit();
            return result;
        }
    }
}
