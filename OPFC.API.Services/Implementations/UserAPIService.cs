using AutoMapper;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.User;
using OPFC.API.Services.Interfaces;
using OPFC.Models;
using OPFC.Services.Interfaces;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.API.Services.Implementations
{
    public class UserAPIService : Service, IUserAPIService
    {
        private IUserService _userService = AppHostBase.Instance.Resolve<IUserService>();

        public CreateEventPlannerResponse Post(CreateEventPlannerRequest request)
        {
            var user = Mapper.Map<UserDTO>(request.User);

            var result = _userService.CreateUser(Mapper.Map<User>(user));
            return new CreateEventPlannerResponse
            {
                User = Mapper.Map<UserDTO>(result)
            };
        }
    }
}
