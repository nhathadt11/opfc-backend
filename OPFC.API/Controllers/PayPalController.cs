using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPFC.Services.UnitOfWork;
using OPFC.API.ServiceModel.PayPal;
using AutoMapper;
using OPFC.API.DTO.RequestPaypalObject;
using OPFC.Services.Interfaces;

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
        public IActionResult CreatePayment(CreatePaymentRequest request)
        {
            try
            {
                var payment = _serviceUow.PaypalService.CreatePayment(request, "http://localhost:5000/Paypal/ExecutePayment", "http://localhost:5000/PayPal/Cancel", "sale");

                //return new JsonResult(payment);
                //RedirectResult redirect = new RedirectToPageResult();

                return Redirect(payment.links[1].href);
            }
            catch(Exception ex)
            {
                throw;
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
                var payment = _serviceUow.PaypalService.ExecutePayment(paymentId, PayerID);
                //_serviceUow.OrderService.CreateOrder();
			}
            catch
			{
                return Redirect("http://google.com.vn");
			}


			return Redirect("https://opfc-frontend.surge.sh/profile/event-planner/order/1");
        }


        [HttpGet("Token")]
        [AllowAnonymous]
        public IActionResult GetResult()
        {
            PaypalTrans paypal = new PaypalTrans();
            string clientId = "Aaxoz6HfIQVkm_X09U0gtICiTCvrV_tbUjwtig3doeAzC27fJtbn77f-UglDxKMZy-DpKRYOLBDwsNCk";
            string secret = "EORe1azVVTNpHDQPbjCmP1GrgewEW3ATVwfH0Np044CuXQypiX_RqzQO0SWd48UuwMMhO8QOLiiDIuJn";

            try
            {
                return Ok(paypal.GetToken(clientId, secret).Result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
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
