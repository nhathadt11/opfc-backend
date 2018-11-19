using System;
using System.Collections.Generic;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.Menu
{
    [EnableCors("*", "*")]
    [Route("/Menu/CreateMenu/", "POST")]
    public class CreateMenuRequest : IReturn<CreateMenuResponse>
    {
        public string MenuName { get; set; }
        public string Description { get; set; }
        public int ServingNumber { get; set; }
        public decimal Price { get; set; }
        public List<string> Photos { get; set; }
        public List<long> EventTypeIds { get; set; }
        public List<long> MealIds { get; set; }
        public List<string> Tags { get; set; }
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
        public long Id { get; set; }
        public string MenuName { get; set; }
        public string Description { get; set; }
        public int ServingNumber { get; set; }
        public decimal Price { get; set; }
        public List<string> Photos { get; set; }
        public List<long> EventTypeIds { get; set; }
        public List<long> CategoryIds { get; set; }
        public List<long> MealIds { get; set; }
        public List<string> Tags { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/Menu/GetAllMenu/", "GET")]
    public class GetAllMenuRequest : IReturn<GetAllMenuResponse>
    {
    }

    [EnableCors("*", "*")]
    [Route("/Menu/DeleteMenu/", "POST")]
    public class DeleteMenuRequest : IReturn<DeleteMenuResponse>
    {
        public MenuDTO Menu { get; set; }
    }
}
