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
            return DbSet.DefaultIfEmpty().ToList();
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
