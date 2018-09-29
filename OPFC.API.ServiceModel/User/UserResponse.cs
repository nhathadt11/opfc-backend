using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.User
{
    public class GetUserByIdResponse
    {
        ResponseStatus ResponseStatus { get; set; }

        UserDTO User { get; set; }
    }
}
