using System;
using OPFC.API.ServiceModel.PayPal;
using PayPal.Api;
namespace OPFC.Services.Interfaces
{
    public interface IPaypalService
    {
        Payment CreatePayment(CreatePaymentRequest request, string returnUrl, string cancelUrl, string intent);

        Payment ExecutePayment(string paymentId,string payerID);
    }
}
