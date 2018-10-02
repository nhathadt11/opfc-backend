using OPFC.API.ServiceModel.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Interfaces
{
    public interface IUserAPIService
    {
        CreateEventPlannerResponse Post(CreateEventPlannerRequest request);
    }
}
