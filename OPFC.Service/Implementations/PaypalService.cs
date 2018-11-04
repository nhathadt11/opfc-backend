using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.ConfigOptions;
using OPFC.Services.Interfaces;
using PayPal.Api;
using OPFC.Constants;
using OPFC.API.ServiceModel.PayPal;
using System.Linq;
using System.Collections;
using OPFC.API.ServiceModel.Order;
using OPFC.Services.UnitOfWork;
using OPFC.Models;
using Transaction = PayPal.Api.Transaction;

namespace OPFC.Services.Implementations
{
    public class PaypalService : IPaypalService
    {
        private readonly IServiceUow _serviceUow = ServiceStack.AppHostBase.Instance.TryResolve<IServiceUow>();

        private readonly PayPalAuthOptions _options;

        private readonly IOpfcUow _opfcUow;

        public PaypalService(IOptions<PayPalAuthOptions> options)
        {
            _options = options.Value;
        }

        public PaypalService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public Payment CreatePayment(CreateOrderRequest request, string returnUrl, string cancelUrl, string intent)
        {
            var token = new OAuthTokenCredential(PaypalConfig.CLIENT_ID, PaypalConfig.CLIENT_SECRET).GetAccessToken();
            var apiContext = new APIContext(token);

            var menuList = _opfcUow.MenuRepository
                                   .GetAll()
                                   .Where(m => request.RequestMenuList.Select(rm => rm.MenuId).Contains(m.Id))
                                   .ToList();

            var total = (decimal)0;
            var items = new ItemList();
            items.items = new List<Item>();
            for (int i = 0; i < menuList.Count; i++)
            {
                items.items.Add(new Item
                {
                    quantity = "1",
                    tax = "0",
                    price = menuList[i].Price.ToString(),
                    description = request.RequestMenuList[i].Note,
                    currency = "USD",
                    sku = request.RequestMenuList[i].MenuId.ToString()
                });

                total += menuList[i].Price;
            }

            var payment = new Payment()
            {
                intent = "sale",
                payer = new Payer() { payment_method = "paypal" },

                redirect_urls = new RedirectUrls()
                {
                    cancel_url = cancelUrl,
                    return_url = returnUrl
                },
                transactions = new List<Transaction>(){
                    new Transaction(){
                        amount = new Amount(){
                            total = total.ToString(),
                            currency="USD"
                        },
                        item_list = items,
                        description = request.UserId + "||" + request.EventId
                    },
                }
        };

            payment = payment.Create(apiContext);

            return payment;
        }

        public Payment ExecutePayment(string paymentId, string payerID)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(PaypalConfig.CLIENT_ID, PaypalConfig.CLIENT_SECRET).GetAccessToken());

            var paymentExecution = new PaymentExecution() { payer_id = payerID };

            var executedPayment = new Payment() { id = paymentId }.Execute(apiContext, paymentExecution);

            return executedPayment;
        }

        public Payment GetPaymentDetail(string paymentId)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(PaypalConfig.CLIENT_ID, PaypalConfig.CLIENT_SECRET).GetAccessToken());

            var payment = Payment.Get(apiContext, paymentId);

            return payment;
        }


       
        public long SaveOrderAndExecutePayment(string paymentId, string payperID)
        {
            using (var scope = new System.Transactions.TransactionScope())
            {
                var paymentDetail = GetPaymentDetail(paymentId);

                var des = paymentDetail.transactions[0].description;
                List<String> userAndEventId = des.Split("||").ToList();
                var userId = long.Parse(userAndEventId[0]);
                var eventId = long.Parse(userAndEventId[1]);

                var requestMenuList = paymentDetail.transactions[0].item_list.items
                                                   .Select(item => new RequestOrderItem
                {
                    MenuId = long.Parse(item.sku),
                    Note = item.description,
                    Quantity = int.Parse(item.quantity)
                }).ToList();


                var execute = ExecutePayment(paymentId, payperID);

                var orderRequest = new CreateOrderRequest
                {
                    EventId = eventId,
                    UserId = userId,
                    RequestMenuList = requestMenuList,
                    PaymentId = paymentDetail.id,
                    SaleId = execute.transactions[0].related_resources[0].sale.id
                };

                var createdOrder = _serviceUow.OrderService.CreateOrder(orderRequest);

                scope.Complete();

                return createdOrder.OrderId;
            }

        }
    }
}
