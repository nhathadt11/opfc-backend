using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.Services.UnitOfWork;
using OPFC.API.ServiceModel.PayPal;
using AutoMapper;
using OPFC.API.DTO.RequestPaypalObject;
using OPFC.Services.Interfaces;
using OPFC.API.ServiceModel.Order;
using PayPal.Api;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        [HttpPost("CreatePayment")]
        [AllowAnonymous]
        public IActionResult CreatePayment(CreateOrderRequest request)
        {
            try
            {
                var payment = _serviceUow.PaypalService.CreatePayment(request, "http://localhost:5000/Paypal/ExecutePayment", "http://localhost:5000/PayPal/Cancel", "sale");

                return Created("CreatePayment", new { redirect = payment.links[1].href });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ExecutePayment")]
        [AllowAnonymous]
        public IActionResult ExecutePayment([FromQuery(Name = "PayerID")] string PayerID,
                                            [FromQuery(Name = "paymentId")] string paymentId,
                                            [FromQuery(Name = "token")] string token)
        {
			try
			{
                var orderId = _serviceUow.PaypalService.SaveOrderAndExecutePayment(paymentId, PayerID);

                return Redirect($"https://opfc-frontend.surge.sh/profile/event-planner/order/{orderId}");
            }
            catch
			{
                return Redirect("http://google.com.vn");
			}
        }


        [HttpPost("refund")]
        [AllowAnonymous]
        public IActionResult Refund()
        {


            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
