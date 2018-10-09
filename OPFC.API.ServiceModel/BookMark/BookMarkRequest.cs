using System;
using OPFC.API.DTO;
using ServiceStack;
using System.Collections.Generic;
namespace OPFC.API.ServiceModel.BookMark
{
    [EnableCors("*", "*")]
    [Route("/BookMark/CreateBookMark/", "POST")]
    public class CreateBookMarkRequest : IReturn<CreateBookMarkResponse>
    {
        public BookMarkDTO BookMark { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/BookMark/UpdateBookMark/", "POST")]
    public class UpdateBookMarkRequest: IReturn<UpdateBookMarkResponse>
    {
        public BookMarkDTO BookMark { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/BookMark/DeleteBookMark/", "POST")]
    public class DeleteBookMarkRequest : IReturn<DeleteBookMarkResponse>
    {
        public BookMarkDTO BookMark { get; set; }
    }

    [EnableCors("*", "*")]
    [Route("/BookMark/GetAllBookMark/", "GET")]
    public class GetAllBookMarkRequest: IReturn<GetAllBookMarkResponse>
    {

    }
}
