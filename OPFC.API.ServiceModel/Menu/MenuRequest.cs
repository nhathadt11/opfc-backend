using System;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.Menu
{
    [Route("/Menu/CreateMenu/", "POST")]
    public class CreateMenuRequest : IReturn<CreateMenuResponse>
    {
        public MenuDTO Menu { get; set; } 
    }

    [Route("/Menu/GetMenuById/{id}", "GET")]
    public class GetMenuByIdRequest : IReturn<GetMenuByIdResponse>
    {
        public long Id { get; set; }
    }

    [Route("/Menu/UpdateMenu/", "POST")]
    public class UpdateMenuRequest : IReturn<UpdateMenuResponse>
    {
        public MenuDTO Menu { get; set; }
    }
}
