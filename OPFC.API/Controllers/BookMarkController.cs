using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.BookMark;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Route("/[controller]")]
    [ApiController]
    public class BookMarkController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        [Route("/BookMark/CreateBookMark/")]
        public CreateBookMarkResponse Post(CreateBookMarkRequest request)
        {
            var bookMark = Mapper.Map<BookMark>(request.BookMark);

            return new CreateBookMarkResponse
            {
                BookMark = Mapper.Map<BookMarkDTO>(_serviceUow.BookMarkService.CreateBookMark(bookMark))
            };
        }

        [HttpGet]
        [Route("/BookMark/GetAllBookMark/")]
        public GetAllBookMarkResponse Get()
        {
            var bookMarks = Mapper.Map<List<BookMarkDTO>>(_serviceUow.BookMarkService.GetAllBookMark());

            return new GetAllBookMarkResponse
            {
                BookMarks = bookMarks
            };
        }

        [HttpPut]
        [Route("/BookMark/UpdateBookMark/")]
        public UpdateBookMarkResponse Post(UpdateBookMarkRequest request)
        {
            var bookMark = Mapper.Map<BookMark>(request.BookMark);

            return new UpdateBookMarkResponse
            {
                BookMark = Mapper.Map<BookMarkDTO>(_serviceUow.BookMarkService.UpdateBookMark(bookMark))
            };
        }

        [HttpDelete]
        [Route("/BookMark/RemoveBookMark/{id}")]
        public RemoveBookMarkResponse Post(RemoveBookMarkRequest request)
        {
            var bookMark = Mapper.Map<BookMark>(request);

            return new RemoveBookMarkResponse
            {
                BookMark = Mapper.Map<BookMarkDTO>(_serviceUow.BookMarkService.RemoveBookMark(bookMark))
            };
        }
    }
}