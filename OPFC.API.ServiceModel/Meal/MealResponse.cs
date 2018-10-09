using System;
using System.Collections.Generic;
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

    public class GetAllMealResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public List<MealDTO> Meals { get; set; }
    }

    public class UpdateMealResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public MealDTO Meal { get; set; }
    }

    public class DeleteMealResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public MealDTO Meal { get; set; }
    }
}
