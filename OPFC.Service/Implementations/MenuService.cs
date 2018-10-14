using System;
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
            var result = _opfcUow.MenuRepository.GetAllMenu();

            return result;
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
                    IsDeleted = false
                })
                .ToList();

            _opfcUow.MenuMealRepository.CreateRange(menuMealList);
            _opfcUow.MenuEventTypeRepository.CreateRange(menuEventTypeList);
            _opfcUow.Commit();

            return createdMenu;
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
