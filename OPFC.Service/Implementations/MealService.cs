using System;
using OPFC.Models;
using OPFC.Services.Interfaces;
using OPFC.Repositories.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace OPFC.Services.Implementations
{
    public class MealService : IMealService
    {
        /// <summary>
        /// The opfc uow.
        /// </summary>
        private readonly IOpfcUow _opfcUow;

        public MealService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public Meal CreateMeal(Meal meal)
        {
            meal.LastUpdated = DateTime.UtcNow;

            var result = _opfcUow.MealRepository.CreateMeal(meal);
            _opfcUow.Commit();
            return result;
        }

        public List<Meal> GetAllMeal()
        {
            return _opfcUow.MealRepository.GetAll().ToList();
        }

        public void DeleteMeal(Meal meal)
        {   
            meal.IsDeleted = true;
            if (UpdateMeal(meal) == null)
            {
                throw new Exception("Event could not be deleted.");
            }
        }

        public Meal GetMealById(long id)
        {
            return _opfcUow.MealRepository.GetById(id);
        }

        public Meal UpdateMeal(Meal meal)
        {
            var result = _opfcUow.MealRepository.UpdateMeal(meal);
            _opfcUow.Commit();
            return result;
        }
        
        
    }
}
