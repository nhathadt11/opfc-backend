using System;
using OPFC.API.DTO;
using ServiceStack;
using System.Collections.Generic;
namespace OPFC.API.ServiceModel.BookMark
{
    public class CreateBookMarkRequest : IReturn<CreateBookMarkResponse>
    {
        public BookMarkDTO BookMark { get; set; }
    }

    public class RemoveBookMarkRequest : IReturn<RemoveBookMarkResponse>
    {
        public long BookMarkId { get; set; }
    }
}
