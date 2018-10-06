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

        
        [Route("/BookMark/CreateBookMark/")]
        public CreateBookMarkResponse Post(CreateBookMarkRequest request)
        {
            var bookMark = Mapper.Map<BookMark>(request.BookMark);

            return new CreateBookMarkResponse
            {
                BookMark = Mapper.Map<BookMarkDTO>(_serviceUow.BookMarkService.CreateBookMark(bookMark))
            };
        }

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