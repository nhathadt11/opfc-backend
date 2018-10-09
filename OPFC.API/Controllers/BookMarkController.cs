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
    [Route("/[controller]")]
    [ApiController]
    public class BookMarkController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        [Route("/BookMark")]
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
        [Route("/BookMark")]
        public ActionResult GetAll()
        {
            var bookMarks = _serviceUow.BookMarkService.GetAllBookMark();

            return Ok(Mapper.Map<List<BookMarkDTO>>(bookMarks));
        }

        [HttpPut]
        [Route("/BookMark")]
        public ActionResult Update(UpdateBookMarkRequest request)
        {
            try
            {
                var bookMark = Mapper.Map<BookMark>(request.BookMark);

                var result = _serviceUow.BookMarkService.UpdateBookMark(Mapper.Map<BookMark>(bookMark));

                return Ok(Mapper.Map<BookMarkDTO>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete]
        [Route("/BookMark/{id}")]
        public ActionResult Delete(String id)
        {
            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            var bookMark = _serviceUow.BookMarkService.GetBookMarkbyId(long.Parse(id));

            if (bookMark == null)
                return NotFound(new { Message = "Could not find bookmark" });

            try
            {
                _serviceUow.BookMarkService.DeleteBookMark(bookMark);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(new {ex.Message});
            }

        }

        [HttpDelete]
        [Route("/BookMark")]
        public ActionResult Delete(DeleteBookMarkRequest request)
        {
            try
            {
                var bookmark = Mapper.Map<BookMarkDTO>(request.BookMark);


                if (string.IsNullOrEmpty(bookmark.BookMarkId.ToString()) || !Regex.IsMatch((bookmark.BookMarkId.ToString()), "^\\d+$"))
                    return NotFound(new { Message = "Invalid Id" });


                var foundBookMark = _serviceUow.BookMarkService.GetBookMarkbyId(bookmark.BookMarkId);
                if (foundBookMark == null)
                {
                    return NotFound(new { Message = " could not find BookMark to delete" });
                }

                foundBookMark.IsDeleted = true;

                try
                {
                    _serviceUow.BookMarkService.UpdateBookMark(foundBookMark);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(new { ex.Message });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}