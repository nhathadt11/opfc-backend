using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace OPFC.Repositories.Implementations
{
    public class MenuRepository :EFRepository<Menu>, IMenuRepository
    {
        public MenuRepository(DbContext dbContext) : base(dbContext)
        {
        }

        public Menu CreateMenu(Menu menu)
        {
            return DbSet.Add(menu).Entity;
        }

        public Menu UpdateMenu(Menu menu)
        {
            return DbSet.Update(menu).Entity;
        }
    }
}
