﻿using OPFC.API.DTO;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.ServiceModel.User
{
    public class AuthenticationResponse
    {
        public string Message { get; set; }

        public string Token { get; set; }
    }

    public class GetUserByIdResponse
    {
        ResponseStatus ResponseStatus { get; set; }

        UserDTO User { get; set; }
    }

    public class CreateEventPlannerResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public UserDTO User { get; set; }
    }
}
