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
        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            try
            {
                var found = Mapper.Map<List<BookMarkDTO>>(_serviceUow.BookMarkService.GetBookMarkbyId(id));
                if (found == null)
                {
                    return NotFound("Bookmark could not be found.");
                }
                return Ok(found);
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

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            var bookMark = _serviceUow.BookMarkService.GetBookMarkbyId(id);

            if (bookMark == null)
                return NotFound(new { Message = "Could not find bookmark" });

            try
            {
                _serviceUow.BookMarkService.DeleteBookMark(bookMark);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}