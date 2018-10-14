using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Repositories.Implementations
{
    public class MenuEventTypeRepository : EFRepository<MenuEventType>, IMenuEventTypeRepository
    {
        public MenuEventTypeRepository(DbContext dbContext) : base(dbContext)
        {
        }


        public List<MenuEventType> GetByMenuId(long id)
        {
            return DbSet.Where(e => e.MenuId == id && e.IsDeleted == false).ToList();
        }

        public void CreateRange(List<MenuEventType> menuEventTypeList)
        {
            DbSet.AddRange(menuEventTypeList);
        }
    }
}