using System;
using OPFC.Models;
using OPFC.Services.Interfaces;
using OPFC.Repositories.UnitOfWork;
using System.Collections.Generic;

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
