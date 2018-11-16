using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.PrivateRating;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Route("[controller]")]
    [ApiController]
    public class PrivateRatingController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var privateratingList = _serviceUow.PrivateRatingService.GetAllPrivateRating();
                return Ok(Mapper.Map<List<PrivateRatingDTO>>(privateratingList));
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
                var found = _serviceUow.PrivateRatingService.GetPrivateRatingById(id);
                if (found == null)
                {
                    return NotFound("Rating could not be found.");
                }
                return Ok(Mapper.Map<PrivateRatingDTO>(found));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("OrderLine/{id}")]
        public IActionResult GetByTransactionId(long id)
        {
            try
            {
                if (!_serviceUow.MenuService.Exists(id)) throw new Exception("OrderLine could not be found.");

                var retrieved = _serviceUow.PrivateRatingService.GetAllPrivateRating().Where(r => r.Id == id);
                var returnPrivateRatingList = Mapper.Map<List<PrivateRatingDTO>>(retrieved);

                returnPrivateRatingList.ForEach(r =>
                {

                });
                return Ok(returnPrivateRatingList);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("User/{userId}/OrderLine/{orderLineId}")]
        public IActionResult Create(long userId, long orderLineId, CreatePrivateRatingRequest request)
        {
            try
            {
                request.PrivateRating.OrderLineId = orderLineId;
                request.PrivateRating.UserId = userId;
                var privaterating = Mapper.Map<PrivateRating>(request.PrivateRating);
                var created = _serviceUow.PrivateRatingService.CreatePrivateRating(privaterating);
                var returnRating = Mapper.Map<PrivateRatingDTO>(created);

                return Created("Private Rating", created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, UpdatePrivateRatingRequest request)
        {
            try
            {
                var found = _serviceUow.PrivateRatingService.GetPrivateRatingById(id);
                if (found == null)
                {
                    return NotFound("Rating could not be found.");
                }

                var privaterating = Mapper.Map<PrivateRating>(request.PrivateRating);
                return Ok(Mapper.Map<PrivateRatingDTO>(_serviceUow.PrivateRatingService.UpdatePrivateRating(privaterating)));
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
                var found = _serviceUow.PrivateRatingService.GetPrivateRatingById(id);
                if (found == null)
                {
                    return NotFound("Rating could not be found.");
                }

                _serviceUow.PrivateRatingService.DeletePrivateRatingById(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}




