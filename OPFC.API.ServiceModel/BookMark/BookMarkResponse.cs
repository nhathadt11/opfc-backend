using System;
using OPFC.API.DTO;
using ServiceStack;
namespace OPFC.API.ServiceModel.BookMark
{
    public class CreateBookMarkResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public BookMarkDTO BookMark { get; set; }
    }

    public class RemoveBookMarkResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public BookMarkDTO BookMark { get; set; }
    }
}
