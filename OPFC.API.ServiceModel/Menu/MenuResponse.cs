using System;
using System.Collections.Generic;
using OPFC.API.DTO;
using ServiceStack;

namespace OPFC.API.ServiceModel.Menu
{
    public class CreateMenuResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public MenuDTO Menu { get; set; }


    }
    public class GetMenuByIdResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public MenuDTO Menu { get; set; }
    }

    public class GetAllMenuResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public List<MenuDTO> Menues { get; set; }
    }

    public class UpdateMenuResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public MenuDTO Menu { get; set; }
    }

    public class DeleteMenuResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public MenuDTO Menu { get; set; }
    }
}
