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
        public IActionResult CreatePayment()
        {
            try
            {
                var payment = _serviceUow.PaypalService.CreatePayment(100, "http://localhost:5000/Paypal/ExecutePayment", "http://localhost:5000/PayPal/Cancel", "sale");

                //return new JsonResult(payment);
                //RedirectResult redirect = new RedirectToPageResult();

                return new JsonResult(payment);
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

            var payment = _serviceUow.PaypalService.ExecutePayment(paymentId, PayerID);


            return new JsonResult(payment);
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

        [HttpPost("Trans")]
        [AllowAnonymous]
        public IActionResult Trans(TransRequest request)
        {
            PaypalTrans paypal = new PaypalTrans();
            var parameter = Mapper.Map<RequestParameter>(request.Parameter);
          //  string token = "A21AAE_cNeEq7_vKhxIBTGn9lYFx9ixg_xaWb_EsXlDxWlq_QyXM5PVtUG7ET8Wqy5RdN7jGFItSzjMkaf3_KJh8P_43aGnGw";
            try
            {
                return Ok(paypal.PaypalTran(parameter).Result);
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
