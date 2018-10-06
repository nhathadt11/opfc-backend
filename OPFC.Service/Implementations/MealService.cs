using System;
using OPFC.Models;
using OPFC.Services.Interfaces;
using OPFC.Repositories.UnitOfWork;
using System.Collections.Generic;
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
            try
            {
                meal.LastUpdated = DateTime.UtcNow;

                var result = _opfcUow.MealRepository.CreateMeal(meal);
                _opfcUow.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Meal GetMealById(long id)
        {
            try
            {
                return _opfcUow.MealRepository.GetById(id);
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public Meal UpdateMeal(Meal meal)
        {
            try
            {
                var result = _opfcUow.MealRepository.UpdateMeal(meal);
                _opfcUow.Commit();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
