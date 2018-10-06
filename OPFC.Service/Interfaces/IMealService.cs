using System;
using System.Collections.Generic;
using OPFC.Models;
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
