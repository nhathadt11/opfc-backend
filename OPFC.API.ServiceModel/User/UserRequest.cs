using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.User
{
    [EnableCors("*", "*")]
    [Route("/User/GetUserById/", "GET")]
    public class GetUserByIdRequest : IReturn<GetUserByIdResponse>
    {
        public long Id { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/User/CreateEventPlanner/", "POST")]
    public class CreateEventPlannerRequest : IReturn<GetUserByIdResponse>
    {
        public UserDTO User;
    }
}
