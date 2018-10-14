using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class MenuMealRepository : EFRepository<MenuMeal>, IMenuMealRepository
    {
        public MenuMealRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public List<MenuMeal> GetByMenuId(long id)
        {
            return DbSet.Where(m => m.MenuId == id && m.IsDeleted == false).ToList();
        }

        public void CreateRange(List<MenuMeal> menuMealList)
        {
            DbSet.AddRange(menuMealList);
        }
    }
}