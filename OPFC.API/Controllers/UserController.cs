using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.Models;
using OPFC.Services.Interfaces;
using ServiceStack;

namespace OPFC.API.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        // Dependency Injection. We should communicate with interface only
        private readonly IUserService _userService = AppHostBase.Instance.Resolve<IUserService>();

        //POST api/user/GetUserById/{id}
        [HttpPost("GetUserById/{id}", Name = "GetUserById")]
        public ActionResult<User> GetUserById(long id)
        {
            var user = _userService.GetUserById(id);
            return user;
        }
    }
}