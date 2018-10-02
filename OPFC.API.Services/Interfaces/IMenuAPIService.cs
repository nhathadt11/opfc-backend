using System;
using OPFC.API.ServiceModel.Menu;
namespace OPFC.API.Services.Interfaces
{
    public interface IMenuAPIService
    {
        CreateMenuResponse Post(CreateMenuRequest request);

        UpdateMenuResponse Post(UpdateMenuRequest request);

        GetMenuByIdResponse Get(GetMenuByIdRequest request);
    }
}
