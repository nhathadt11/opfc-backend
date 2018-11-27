using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace OPFC.Repositories.Implementations
{
    public class MenuRepository : EFRepository<Menu>, IMenuRepository
    {
        public MenuRepository(DbContext dbContext) : base(dbContext) { }

        public Menu CreateMenu(Menu menu)
        {
            return DbSet.Add(menu).Entity;
        }

        public List<Menu> GetAllMenu()
        {
            return DbSet.Where(m => m.IsActive == true && m.IsDeleted == false).ToList();
        }

        public List<Menu> GetAllMenuWithCollaborative()
        {
            return DbSet.Where(m => m.IsActive == true && m.IsDeleted == false)
                        .Include(m => m.MenuEventTypeList)
                        .Include(m => m.MenuMealList)
                        .Include(m => m.MenuCategoryList)
                        .ToList();
        }

        public List<Menu> GetAllByBrandId(long id)
        {
            return DbSet.Where(m => m.BrandId == id).ToList();
        }

        public List<Menu> GetAllByBrandIds(List<long> brandIds)
        {
            return DbSet.Where(m => brandIds.Contains(m.BrandId)).ToList();
        }

        public Menu GetMenuById(long MenuId)
        {
            return DbSet.SingleOrDefault(m => m.Id == MenuId && m.IsDeleted == false);
        }

        public Menu UpdateMenu(Menu menu)
        {
            return DbSet.Update(menu).Entity;
        }

        public List<Menu> GetAllMenuByIdsWithCollaborative(List<long> menuIds)
        {
            return DbSet.Where(m => m.IsActive == true && m.IsDeleted == false
                                && menuIds.Contains(m.Id))
                        .Include(m => m.MenuEventTypeList)
                        .ToList();
        }
    }
}
