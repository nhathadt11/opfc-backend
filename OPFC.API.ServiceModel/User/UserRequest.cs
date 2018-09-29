using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.User
{
    [Route("/User/GetUserById/", "GET")]
    public class GetUserByIdRequest : IReturn<GetUserByIdResponse>
    {
        public long Id { get; set; }
    }
}
