using System;
using OPFC.API.ServiceModel.Meal;

namespace OPFC.API.Services.Interfaces
{
    public interface IMealAPIService
    {
        CreateMealResponse Post(CreateMealRequest request);
    }
}
