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
using OPFC.API.ServiceModel.Order;
using OPFC.API.ServiceModel.Order;
using OPFC.Models;
using OPFC.Models;
using OPFC.Services.UnitOfWork;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost]
        public ActionResult Create(CreateOrderRequest orderRequest)
        {
            try
            {
                Order created = _serviceUow.OrderService.CreateOrder(orderRequest);
                return Created("/Order", created);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/Order")]
        public ActionResult GetAll()
        {
            var orders = _serviceUow.OrderService.GetAllOrder();

            return Ok(Mapper.Map<List<OrderDTO>>(orders));

        }

        [HttpGet("/Order/{id}")]
        public ActionResult Get(string id)
        {

            if (string.IsNullOrEmpty(id) || !Regex.IsMatch(id, "^\\d+$"))
                return BadRequest(new { Message = "Invalid Id" });

            var order = _serviceUow.OrderService.GetOrderById(long.Parse(id));
            if (order == null)
            {
                return NotFound(new { Message = "Could not find Order" });
            }
            return Ok(Mapper.Map<OrderDTO>(order));
        }

        [HttpPut("/Order")]
        public ActionResult Update(UpdateOrderRequest request)
        {
            try
            {
                var order = Mapper.Map<MealDTO>(request.order);

                var result = _serviceUow.OrderService.UpdateOrder(Mapper.Map<Order>(order));

                return Ok(Mapper.Map<OrderDTO>(result));
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }

        [HttpDelete("/Order")]
        public ActionResult Delete(DeleteOrderRequest request)
        {
            try
            {
                var order = Mapper.Map<OrderDTO>(request.order);


                if (string.IsNullOrEmpty(order.OrderId.ToString()) || !Regex.IsMatch((order.OrderId.ToString()), "^\\d+$"))
                    return NotFound(new { Message = "Invalid Id" });


                var foundOrder = _serviceUow.OrderService.GetOrderById(order.OrderId);
                if (foundOrder == null)
                {
                    return NotFound(new { Message = " could not find Order to delete" });
                }

                foundOrder.IsDeleted = true;

                try
                {
                    _serviceUow.OrderService.UpdateOrder(foundOrder);
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