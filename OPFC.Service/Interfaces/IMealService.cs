using System;
using System.Collections.Generic;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Services.Interfaces
{
    public interface IMealService
    {
        Meal CreateMeal(Meal meal);
        Meal GetMealById(long id);
        Meal UpdateMeal(Meal meal);
        List<Meal> GetAllMeal();

    }
}
