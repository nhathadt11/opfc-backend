using System;
using System.Collections.Generic;
using OPFC.API.ServiceModel.Menu;
using OPFC.Models;
namespace OPFC.Services.Interfaces
{
    public interface IMenuService
    {
        Menu CreateMenu(Menu menu);
        Menu GetMenuById(long id);
        Menu UpdateMenu(Menu menu);
        List<Menu> GetAllMenu();
        void DeleteMenuById(long id);
        List<Menu> GetAllMenuByBrandId(long id);
        Menu CreateMenuByBrand(long brandId, CreateMenuRequest menu);
        Menu UpdateMenuByBrand(long brandId, long menuId, UpdateMenuRequest menu);
        bool Exists(long id);
        List<Menu> GetAllBookmarkedMenuByUserId(long userId);
    }
}
