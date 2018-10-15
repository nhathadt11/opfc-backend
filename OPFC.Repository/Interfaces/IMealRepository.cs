using System;
using OPFC.Models;
using System.Collections.Generic;

namespace OPFC.Repositories.Interfaces
{
    public interface IMealRepository : IRepository<Meal>
    {
        Meal CreateMeal(Meal meal);

        Meal UpdateMeal(Meal meal);

        Meal GetMealById(long id);

        List<Meal> GetAllMeal();

        bool isExist(long id);
    }
}
