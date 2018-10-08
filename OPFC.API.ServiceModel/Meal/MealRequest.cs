using System;
using ServiceStack;
using OPFC.API.DTO;

namespace OPFC.API.ServiceModel.Meal
{
    [EnableCors("*", "*")]
    [Route("/Brand/CreateBrand/", "POST")]
    public class CreateMealRequest : IReturn<CreateMealResponse>
    {
        public MealDTO Meal { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Meal/GetAllMeal/", "Get")]
    public class GetAllMealRequest : IReturn<GetAllMealResponse>
    {

    }

    [EnableCors("*", "*")]
    [Route("/Meal/GetMealById/", "Get")]
    public class GetMealByIdRequest :IReturn<GetMealByIdResponse>
    {
        public long Id { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Meal/UpdateMeal/", "POST")]
    public class UpdateMealRequest : IReturn<UpdateMealResponse>
    {
        public MealDTO Meal { get; set; }
    }
}
