using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.BookMark;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class BookMarkController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        public ActionResult Create(CreateBookMarkRequest request)
        {
            try
            {
                var bookMark = Mapper.Map<BookMarkDTO>(request.BookMark);
                var result = _serviceUow.BookMarkService.CreateBookMark(Mapper.Map<BookMark>(bookMark));

                return Created("/BookMark", Mapper.Map<BookMarkDTO>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var bookMarks = Mapper.Map<List<BookMarkDTO>>(_serviceUow.BookMarkService.GetAllBookMark());
                return Ok(bookMarks);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Update(long id, UpdateBookMarkRequest request)
        {
            try
            {
                var bookMark = Mapper.Map<BookMark>(request.BookMark);

                var result = _serviceUow.BookMarkService.UpdateBookMark(Mapper.Map<BookMark>(bookMark));

                return Ok(Mapper.Map<BookMarkDTO>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("User/{userId}/Menu/{menuId}")]
        [AllowAnonymous]
        public ActionResult Bookmark(long userId, long menuId)
        {
            // goi bookmark service
            var bookMark = new BookMark
            {
                UserId = userId,
                MenuId = menuId
            };

            try
            {
                var created = _serviceUow.BookMarkService.CreateBookMark(bookMark);
                return Created("Bookmark", created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("User/{userId}/Menu/{menuId}")]
        [AllowAnonymous]
        public ActionResult RemoveBookmark(long userId, long menuId)
        {
            try
            {
                _serviceUow.BookMarkService.RemoveBookmark(userId, menuId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}