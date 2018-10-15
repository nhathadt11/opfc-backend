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
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Dependency Injection. We should communicate with interface only
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost("/User/CreateEventPlanner/")]
        public IActionResult Post(CreateEventPlannerRequest request)
        {
            try
            {
                var user = Mapper.Map<UserDTO>(request.User);
                var result = _serviceUow.UserService.CreateUser(Mapper.Map<User>(user));
                return Ok(Mapper.Map<UserDTO>(result));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("/User/Authenticate/")]
        public IActionResult Authenticate(AuthenticationRequest request)
        {
            try
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateUserRequest request)
        {
            var isUserExist = _serviceUow.UserService.IsUserExist(request.User.Username);
            if (!isUserExist) return NotFound(new { Messsage = "User could not be found." }); 

            try
            {
                var userRequest = Mapper.Map<User>(request.User);
                var userResult = _serviceUow.UserService.Update(userRequest);

                return Ok(Mapper.Map<UserDTO>(userResult));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                User found = _serviceUow.UserService.GetUserById(id);
                if (found == null)
                {
                    return NotFound(new { Messsage = "User could not be found." });
                }

                return Ok(Mapper.Map<UserDTO>(found));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}