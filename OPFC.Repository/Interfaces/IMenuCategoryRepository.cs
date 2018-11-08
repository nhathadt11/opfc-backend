using OPFC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    public interface IMenuCategoryRepository : IRepository<MenuCategory>
    {
        List<MenuCategory> GetAllByMenuIds(List<long> menuIds);
        void CreateRange(List<MenuCategory> menuCategories);
        void RemoveRange(List<MenuCategory> menuCategories);
    }
}
