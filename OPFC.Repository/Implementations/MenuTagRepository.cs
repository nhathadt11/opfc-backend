using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    class MenuTagRepository : EFRepository<MenuTag>, IMenuTagRepository
    {
        public MenuTagRepository(DbContext dbContext) : base(dbContext) { }

        public List<MenuTag> GetAllByMenuId(long menuId)
        {
            return DbSet.ToList();
        }

        public List<MenuTag> GetAllByMenuIds(List<long> menuIds)
        {
            return DbSet.Where(mt => menuIds.Contains(mt.MenuId)).ToList();
        }
    }
}
