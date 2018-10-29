using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.API.DTO;
using OPFC.API.ServiceModel.Order;
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

        [HttpGet]
        public ActionResult GetAll()
        {
            var orders = _serviceUow.OrderService.GetAllOrder();

            return Ok(Mapper.Map<List<OrderDTO>>(orders));

        }

        [HttpGet("{id}")]
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
        
        [HttpGet("Brand/{brandId}")]
        public ActionResult GetBrandOrders(long brandId)
        {
            var brandOrderList = _serviceUow.OrderService.GetBrandOrderByBrandId(brandId);
            return Ok(brandOrderList);
        }
    }
}