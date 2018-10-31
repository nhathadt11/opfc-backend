using Microsoft.EntityFrameworkCore;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Repositories.Implementations
{
    public class CategoryRepository : EFRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DbContext dbContext) : base(dbContext) { }

        public List<Category> GetAll()
        {
            return DbSet.ToList();
        }

        public List<Category> GetAllByIds(List<long> ids)
        {
            return DbSet.Where(c => ids.Contains(c.Id)).ToList();
        }
    }
}
