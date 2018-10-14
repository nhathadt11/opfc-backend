using System;
using System.Collections.Generic;
using System.Net.Sockets;
using OPFC.Models;
using OPFC.Repositories.Interfaces;

namespace OPFC.Services.Interfaces
{
    public interface IMealService
    {
        Meal CreateMeal(Meal meal);
        Meal CreateMealForBrand(Meal meal, long brandId);
        Meal GetMealById(long id);
        Meal UpdateMeal(Meal meal);
        List<Meal> GetAllMeal();
        void DeleteMeal(Meal meal);
        List<Meal> GetAllByBrandId(long brandId);
        bool isExist(long id);
        void DeleteMealById(long id);
        List<Meal> GetAllMealByMenuId(long id);
    }
}
