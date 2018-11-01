using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OPFC.Repositories.Implementations
{
    public class MenuCategoryRepository : EFRepository<MenuCategory>, IMenuCategoryRepository
    {
        public MenuCategoryRepository(DbContext dbContext) : base(dbContext) { }

        public List<MenuCategory> GetAllByMenuIds(List<long> menuIds)
        {
            return DbSet.Where(mc => menuIds.Contains(mc.MenuId)).ToList();
        }
    }
}
