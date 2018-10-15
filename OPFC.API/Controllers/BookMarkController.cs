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
<<<<<<< HEAD
        [Route("/BookMark")]
=======
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        public ActionResult Create(CreateBookMarkRequest request)
        {
            try
            {
                var bookMark = Mapper.Map<BookMarkDTO>(request.BookMark);
<<<<<<< HEAD

=======
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
                var result = _serviceUow.BookMarkService.CreateBookMark(Mapper.Map<BookMark>(bookMark));

                return Created("/BookMark", Mapper.Map<BookMarkDTO>(result));
            }
            catch(Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet]
<<<<<<< HEAD
        [Route("/BookMark")]
        public ActionResult GetAll()
        {
            var bookMarks = _serviceUow.BookMarkService.GetAllBookMark();

            return Ok(Mapper.Map<List<BookMarkDTO>>(bookMarks));
        }

        [HttpPut]
        [Route("/BookMark")]
        public ActionResult Update(UpdateBookMarkRequest request)
=======
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
>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        {
            try
            {
                var bookMark = Mapper.Map<BookMark>(request.BookMark);

                var result = _serviceUow.BookMarkService.UpdateBookMark(Mapper.Map<BookMark>(bookMark));

                return Ok(Mapper.Map<BookMarkDTO>(result));
            }
            catch(Exception ex)
            {
<<<<<<< HEAD
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
=======
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

>>>>>>> 42be1eec49ca3c2199a0e7b1efd191b1b654d298
        }
    }
}