using System;
using OPFC.Models;
namespace OPFC.Services.Interfaces
{
    public interface IMealService
    {
        Meal CreateMeal(Meal meal);
        Meal GetMealById(long id);
        Meal UpdateMeal(Meal meal);

    }
}
