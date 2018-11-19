using System;
using OPFC.API.ServiceModel.Order;
using OPFC.API.ServiceModel.PayPal;
using PayPal.Api;
namespace OPFC.Services.Interfaces
{
    public interface IPaypalService
    {
        Payment CreatePayment(CreateOrderRequest request, string returnUrl, string cancelUrl, string intent);

        Payment ExecutePayment(string paymentId,string payerID);

        Payment GetPaymentDetail(string paymentId);

        long SaveOrderAndExecutePayment( string paymentId, string payperID);

        void Refund(long orderLineId);

        bool Transfer(string gmail, double amount);
    }
}
