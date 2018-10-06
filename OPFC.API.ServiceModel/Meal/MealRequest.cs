using System;
using ServiceStack;
using OPFC.API.DTO;

namespace OPFC.API.ServiceModel.Meal
{
    [Route("/Meal/CreatMeal/", "POST")]
    public class CreateMealRequest : IReturn<CreateMealResponse>
    {
        public MealDTO Meal { get; set; }
    }

    [Route("/Meal/GetMealById/{id}","GET")]
    public class GetMealByIdRequest :IReturn<GetMealByIdResponse>
    {
        public long Id { get; set; }
    }

    [Route("/Meal/UpdateMeal/","POST")]
    public class UpdateMealRequest : IReturn<UpdateMealResponse>
    {
        public MealDTO Meal { get; set; }
    }
}
