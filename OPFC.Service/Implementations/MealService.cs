using System;
using OPFC.Models;
using OPFC.Services.Interfaces;
using OPFC.Repositories.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

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

        public Meal CreateMealForBrand(Meal meal, long brandId)
        {
            using(var scope = new TransactionScope())
            {
                meal.BrandId = brandId;
                var result = CreateMeal(meal);
                _opfcUow.Commit();

                var brandSummary = _opfcUow.BrandSummaryRepository.GetByBrandId(brandId);
                brandSummary.MealCount += 1;
                _opfcUow.BrandSummaryRepository.Update(brandSummary);
                _opfcUow.Commit();

                scope.Complete();
                return result;
            }
        }

        public List<Meal> GetAllMeal()
        {
            return _opfcUow.MealRepository.GetAllMeal();
        }

        public void DeleteMeal(Meal meal)
        {   
            using(var scope = new TransactionScope())
            {
                meal.IsDeleted = true;
                if (UpdateMeal(meal) == null)
                {
                    throw new Exception("Event could not be deleted.");
                }
                _opfcUow.Commit();

                var brandSummary = _opfcUow.BrandSummaryRepository.GetByBrandId(meal.BrandId);
                brandSummary.MealCount -= 1;
                _opfcUow.Commit();

                scope.Complete();
            }
        }

        public List<Meal> GetAllByBrandId(long brandId)
        {
            return _opfcUow.MealRepository
                .GetAllMeal()
                .Where(m => m.BrandId == brandId)
                .ToList();
        }

        public bool isExist(long id)
        {
            return _opfcUow.MealRepository.isExist(id);
        }

        public void DeleteMealById(long id)
        {
            var found = _opfcUow.MealRepository.GetMealById(id);
            found.IsDeleted = true;
            DeleteMeal(found);
        }

        public List<Meal> GetAllMealByMenuId(long id)
        {
            var mealIdList = _opfcUow.MenuMealRepository
                .GetByMenuId(id)
                .Select(m => m.MealId);

            var mealList = _opfcUow.MealRepository
                .GetAllMeal()
                .Where(m => mealIdList.Contains(m.Id))
                .ToList();

            return mealList;
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
