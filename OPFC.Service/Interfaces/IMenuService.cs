using System;
using System.Collections.Generic;
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
    }
}
