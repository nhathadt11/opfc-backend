﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Menu;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Route("/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [Route("/Menu/GetMenuById/{id}")]
        public GetMenuByIdResponse Get(GetMenuByIdRequest request)
        {
            var menu = _serviceUow.MenuService.GetMenuById(request.Id);

            return new GetMenuByIdResponse
            {
                Menu = Mapper.Map<MenuDTO>(menu)
            };
        }

        [Route("/Menu/CreateMenu/")]
        public CreateMenuResponse Post(CreateMenuRequest request)
        {
            var menu = Mapper.Map<Menu>(request);

            return new CreateMenuResponse
            {
                Menu = Mapper.Map<MenuDTO>(_serviceUow.MenuService.CreateMenu(menu))
            };
        }

        [Route("/Menu/UpdateMenu/")]
        public UpdateMenuResponse Post(UpdateMenuRequest request)
        {
            var menu = Mapper.Map<Menu>(request.Menu);

            return new UpdateMenuResponse
            {
                Menu = Mapper.Map<MenuDTO>(_serviceUow.MenuService.UpdateMenu(menu))
            };
        }
    }
}