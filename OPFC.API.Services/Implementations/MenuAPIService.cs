using System;
using AutoMapper;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Menu;
using OPFC.API.ServiceModel.Tasks;
using OPFC.API.Services.Interfaces;
using OPFC.Models;
using OPFC.Services.Interfaces;
using ServiceStack;
namespace OPFC.API.Services.Implementations
{
    public class MenuAPIService : Service, IMenuAPIService
    {
        private IMenuService _menuService = AppHostBase.Instance.Resolve<IMenuService>();

        public GetMenuByIdResponse Get(GetMenuByIdRequest request)
        {
            var menu = _menuService.GetMenuById(request.Id);

            return new GetMenuByIdResponse
            {
                Menu = Mapper.Map<MenuDTO>(menu)
            };
        }

        public CreateMenuResponse Post(CreateMenuRequest request)
        {
            var menu = Mapper.Map<Menu>(request);

            return new CreateMenuResponse
            {
                Menu = Mapper.Map<MenuDTO>(_menuService.CreateMenu(menu))
            };
        }

        public UpdateMenuResponse Post(UpdateMenuRequest request)
        {
            var menu = Mapper.Map<Menu>(request.Menu);

            return new UpdateMenuResponse
            {
                Menu = Mapper.Map<MenuDTO>(_menuService.UpdateMenu(menu))
            };
        }
    }
}
