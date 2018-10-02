using System;
using ServiceStack;
using OPFC.API.DTO;

namespace OPFC.API.ServiceModel.Meal
{
    [Route("/Meal/CreatMeal/", "POST")]
    public class CreateMealRequest : IReturn<CreateMealResponse>
    {
        public long BrandId { get; set; }

        public long Id { get; set; }

        public string MealName { get; set; }

        public string Description { get; set; }

        public long MenuId { get; set; }

        public decimal UnitCost { get; set; }

        public int Quality { get; set; }

        public bool IsSpecial { get; set; }

        public bool IsDeleted { get; set; }

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
