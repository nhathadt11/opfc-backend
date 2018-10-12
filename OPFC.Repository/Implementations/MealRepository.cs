using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace OPFC.Repositories.Implementations
{
    public class MealRepository : EFRepository<Meal>, IMealRepository
    {
        public MealRepository(DbContext dbContext) : base(dbContext) { }

        public Meal CreateMeal(Meal meal)
        {
            return DbSet.Add(meal).Entity;
        }

        public List<Meal> GetAllMeal()
        {
            return DbSet.Where(m => m.IsDeleted == false).ToList();
        }

        public bool isExist(long id)
        {
            return DbSet.Any(m => m.Id == id);
        }

        public Meal GetMealById(long id)
        {
            return DbSet.SingleOrDefault(m => m.Id == id && m.IsDeleted == false);

        }

        public Meal UpdateMeal(Meal meal)
        {
            return DbSet.Update(meal).Entity;
        }
    }
}
