using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.User
{
    public class AuthenticationRequest: IReturn<AuthenticationResponse>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class GetUserByIdRequest : IReturn<GetUserByIdResponse>
    {
        public long Id { get; set; }
    }

    public class CreateEventPlannerRequest : IReturn<GetUserByIdResponse>
    {
        public UserDTO User;
    }

    public class UpdateUserRequest : IReturn<GetUserByIdResponse>
    {
        public UserDTO User;
    }
}
