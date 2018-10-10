using System;
using System.Collections.Generic;
using System.Linq;
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
        
        [HttpGet]
        public IActionResult GetAll()
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

        [HttpPost]
        public IActionResult Create(CreateBookMarkRequest request)
        {
            try
            {
                var bookMark = Mapper.Map<BookMark>(request.BookMark);
                var created = _serviceUow.BookMarkService.CreateBookMark(bookMark);
                return Created("/BookMark", Mapper.Map<BookMarkDTO>(created));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdateBookMarkRequest request)
        {
            try
            {
                var found = _serviceUow.BookMarkService.GetBookMarkbyId(id);
                if (found == null)
                {
                    return NotFound("Bookmark could not be found.");
                }

                var bookMark = Mapper.Map<BookMark>(request.BookMark);                
                return Ok(Mapper.Map<BookMarkDTO>(_serviceUow.BookMarkService.UpdateBookMark(bookMark)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            try
            {
                var found = _serviceUow.BookMarkService.GetBookMarkbyId(id);
                if (found == null)
                {
                    return NotFound("Bookmark could not be found.");
                }

                _serviceUow.BookMarkService.RemoveBookMark(found);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}