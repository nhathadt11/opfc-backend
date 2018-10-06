using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace OPFC.Repositories.Implementations
{
    public class MealRepository : EFRepository<Meal>, IMealRepository
    {
        public MealRepository(DbContext dbContext) : base(dbContext) { }

        public Meal CreateMeal(Meal meal)
        {
            return DbSet.Add(meal).Entity;
        }

        public Meal UpdateMeal(Meal meal)
        {
            return DbSet.Update(meal).Entity;
        }
    }
}
