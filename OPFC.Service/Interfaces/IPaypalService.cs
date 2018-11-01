using System;
using PayPal.Api;
namespace OPFC.Services.Interfaces
{
    public interface IPaypalService
    {
        Payment CreatePayment(decimal amount, string returnUrl, string cancelUrl, string intent);

        Payment ExecutePayment(string paymentId,string payerID);
    }
}
