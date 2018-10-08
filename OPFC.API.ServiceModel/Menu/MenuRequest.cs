using System;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.Menu
{
    [EnableCors("*", "*")]
    [Route("/Menu/CreateMenu/", "POST")]
    public class CreateMenuRequest : IReturn<CreateMenuResponse>
    {
        public MenuDTO Menu { get; set; } 
    }

    [EnableCors("*", "*")]
    [Route("/Menu/GetMenuById/{id}", "GET")]
    public class GetMenuByIdRequest : IReturn<GetMenuByIdResponse>
    {
        public long Id { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Menu/UpdateMenu/", "POST")]
    public class UpdateMenuRequest : IReturn<UpdateMenuResponse>
    {
        public MenuDTO Menu { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Menu/GetAllMenu/", "GET")]
    public class GetAllMenuRequest : IReturn<GetAllMenuResponse>
    {
    }
}
