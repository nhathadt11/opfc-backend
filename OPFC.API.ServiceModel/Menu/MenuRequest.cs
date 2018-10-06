using System;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.Menu
{
    [Route("/Menu/CreateMenu/", "POST")]
    public class CreateMenuRequest : IReturn<CreateMenuResponse>
    {
        public long Id { get; set; }

        public string MenuName { get; set; }

        public string Description { get; set; }

        public int ServingNumber { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public long BrandId { get; set; }
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
