using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.User;
using OPFC.Models;
using OPFC.Services.Interfaces;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Dependency Injection. We should communicate with interface only
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        [Route("/User/CreateEventPlanner/")]
        public CreateEventPlannerResponse Post(CreateEventPlannerRequest request)
        {
            var user = Mapper.Map<UserDTO>(request.User);

            var result = _serviceUow.UserService.CreateUser(Mapper.Map<User>(user));
            return new CreateEventPlannerResponse
            {
                User = Mapper.Map<UserDTO>(result)
            };
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("/User/Authenticate/")]
        public IActionResult Authenticate(AuthenticationRequest request)
        {
            var user = _serviceUow.UserService.Authenticate(request.Username, request.Password);

            if(user == null)
            {
                return BadRequest(new { message = "User name and password is invalid." }); 
            }

            Brand brand = null;
            if (user.UserRoleId == 2)
            {
                brand = _serviceUow.BrandService.GetBrandByUserId(user.Id);
            }

            return Ok(new AuthenticationResponse
            {
                User = Mapper.Map<UserDTO>(user),
                Brand = Mapper.Map<BrandDTO>(brand),
                Token = user.Token
            });
        }
    }
}