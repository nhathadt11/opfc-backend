using System;
using OPFC.API.DTO;
using ServiceStack;
using System.Collections.Generic;
namespace OPFC.API.ServiceModel.BookMark
{
    [EnableCors("*","*")]
    [Route("/BookMark/CreateBookMark/", "Post")]
    public class CreateBookMarkRequest : IReturn<CreateBookMarkResponse>
    {
        public long BookMarkId { get; set; }

        public long UserId { get; set; }

        public long MenuId { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/BookMark/RemoveBookMark/{id}", "Post")]
    public class RemoveBookMarkRequest : IReturn<RemoveBookMarkResponse>
    {
        public long BookMarkId { get; set; }
    }
}
