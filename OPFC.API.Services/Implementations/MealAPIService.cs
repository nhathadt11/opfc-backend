/*using System;
using OPFC.API.DTO;
using OPFC.API.Services.Interfaces;
using OPFC.API.ServiceModel.Meal;
using OPFC.API.Services.Implementations;
using OPFC.Models;
using OPFC.API.ServiceModel.Tasks;
using OPFC.Services.Interfaces;
using ServiceStack;
using AutoMapper;

namespace OPFC.API.Services.Implementations
{
    public class MealAPIService : Service, IMealAPIService
    {
        private IMealService _mealService = AppHostBase.Instance.Resolve<IMealService>();

        public CreateMealResponse Post(CreateMealRequest request)
        {
            var meal = Mapper.Map<Meal>(request);

            return new CreateMealResponse
            {
                Meal = Mapper.Map<MealDTO>(_mealService.CreateMeal(meal))
            };
        }

        public GetMealByIdResponse Get(GetMealByIdRequest request)
        {
            var meal = _mealService.GetMealById(request.Id);

            return new GetMealByIdResponse
            {
                Meal = Mapper.Map<MealDTO>(meal)
            };
        }

        public UpdateMealResponse Post(UpdateMealRequest request)
        {
            var meal = Mapper.Map<Meal>(request.Meal);

            return new UpdateMealResponse
            {
                Meal = Mapper.Map<MealDTO>(_mealService.UpdateMeal(meal))
            };
        }

    }
}*/
