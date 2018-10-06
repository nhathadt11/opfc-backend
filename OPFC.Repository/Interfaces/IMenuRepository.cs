using System;
using OPFC.Models;
namespace OPFC.Repositories.Interfaces
{
    public interface IMenuRepository :IRepository<Menu>
    {
        Menu CreateMenu(Menu menu);

        Menu UpdateMenu(Menu menu);
    }
}
