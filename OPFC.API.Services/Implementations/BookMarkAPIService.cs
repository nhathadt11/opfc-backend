/*using System;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.BookMark;
using OPFC.API.ServiceModel.Tasks;
using OPFC.API.Services.Interfaces;
using OPFC.Services.Interfaces;
using ServiceStack;
using AutoMapper;
using OPFC.Models;

namespace OPFC.API.Services.Implementations
{
    public class BookMarkAPIService : Service, IBookMarkAPIService
    {
        private IBookMarkService _bookMarkService = AppHostBase.Instance.Resolve<IBookMarkService>();

        public CreateBookMarkResponse Post(CreateBookMarkRequest request)
        {
            var bookMark = Mapper.Map<BookMark>(request);

            return new CreateBookMarkResponse
            {
                BookMark = Mapper.Map<BookMarkDTO>(_bookMarkService.CreateBookMark(bookMark))
            };
        }

        public DeleteBookMarkResponse Post(DeleteBookMarkRequest request)
        {
            var bookMark = Mapper.Map<BookMark>(request);

            return new RemoveBookMarkResponse
            {
//                BookMark = Mapper.Map<BookMarkDTO>(_bookMarkService.RemoveBookMark(bookMark))
            };
        }
    }
}*/
