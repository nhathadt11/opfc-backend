using System;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.Meal
{
    public class CreateMealResponse
    {
        public ResponseStatus ResponseStatus { get; set; }
       
        public MealDTO Meal { get; set; }
    }

    public class GetMealByIdResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public MealDTO Meal { get; set; }
    }

    public class UpdateMealResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public MealDTO Meal { get; set; }
    }
}
