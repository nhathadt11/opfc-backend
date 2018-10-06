using System;
using ServiceStack;
using OPFC.API.DTO;

namespace OPFC.API.ServiceModel.Meal
{
    public class CreateMealRequest : IReturn<CreateMealResponse>
    {
        public MealDTO Meal { get; set; }
    }

    public class GetAllMealRequest : IReturn<GetAllMealResponse>
    {

    }

    public class GetMealByIdRequest :IReturn<GetMealByIdResponse>
    {
        public long Id { get; set; }
    }

    public class UpdateMealRequest : IReturn<UpdateMealResponse>
    {
        public MealDTO Meal { get; set; }
    }
}
