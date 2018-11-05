using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.Services.UnitOfWork;
using OPFC.API.ServiceModel.Order;
using OPFC.Constants;

namespace OPFC.API.Controllers
{
    [ServiceStack.EnableCors("*", "*")]
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PayPalController : ControllerBase
    {
        private readonly IServiceUow _serviceUow = ServiceStack.ServiceStackHost.Instance.TryResolve<IServiceUow>();

        [HttpPost("CreatePayment")]
        [AllowAnonymous]
        public IActionResult CreatePayment(CreateOrderRequest request)
        {
            try
            {
                var payment = _serviceUow.PaypalService.CreatePayment
                (
                    request,
                    $"{AppSettings.BACKEND_BASE_URL}/Paypal/ExecutePayment",
                    $"{AppSettings.BACKEND_BASE_URL}/PayPal/Cancel",
                    "sale"
                );

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
                return Redirect($"{AppSettings.FRONTEND_BASE_URL}/profile/event-planner/order/{orderId}");
            }
            catch
			{
                return Redirect("http://google.com.vn");
			}
        }


        [HttpPost("Refund/{orderLineId}")]
        [AllowAnonymous]
        public IActionResult Refund(long orderLineId)
        {
            try
            {
                var orderLine = _serviceUow.OrderLineService.GetOrderLineById(orderLineId);
                var order = _serviceUow.OrderService.GetOrderById(orderLine.OrderId);
                var amount = orderLine.Amount;
                var saleId = order.PaypalSaleRef;

                _serviceUow.PaypalService.Refund(saleId,amount);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
