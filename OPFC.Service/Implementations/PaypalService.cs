using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.ConfigOptions;
using OPFC.Services.Interfaces;
using PayPal.Api;
using OPFC.Constants;

namespace OPFC.Services.Implementations
{
    public class PaypalService : IPaypalService
    {
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

        public Payment CreatePayment(decimal amount, string returnUrl, string cancelUrl, string intent)
        {
            try
            {
                var token = new OAuthTokenCredential(PaypalConfig.CLIENT_ID, PaypalConfig.CLIENT_SECRET).GetAccessToken();
                var apiContext = new APIContext(token);

                var payment = new Payment()
                {
                    intent = "sale",
                    payer = new Payer() { payment_method = "paypal" },

                    redirect_urls = new RedirectUrls()
                    {
                        cancel_url = cancelUrl,
                        return_url = returnUrl
                    },
                    transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            amount = new Amount()
                            {
                                total = "10",
                                currency = "USD"
                            },
                            

                        }
                    }

                };

                payment = payment.Create(apiContext);

                return payment;

            }catch(Exception ex)
            {
                throw ex;
            }

        }

        public Payment ExecutePayment(string paymentId, string payerID)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(PaypalConfig.CLIENT_ID, PaypalConfig.CLIENT_SECRET).GetAccessToken());

            var paymentExecution = new PaymentExecution() { payer_id = payerID };

            var executedPayment = new Payment() { id = paymentId }.Execute(apiContext, paymentExecution);

            return executedPayment;
        }

        private List<Transaction> GetTransactionsList(decimal amount)
        {
            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = GetRandomInvoiceNumber(),
                amount = new Amount()
                {
                    currency = "USD",
                    total = amount.ToString(),
                    details = new Details()
                    {
                        tax = "0",
                        shipping = "0",
                        subtotal = amount.ToString()
                    }
                },
                item_list = new ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                            name = "Payment",
                            currency = "USD",
                            price = amount.ToString(),
                            quantity = "1",
                            sku = "sku"
                        }
                    }
                },
                payee = new Payee
                {
                    // TODO.. Enter the payee email address here
                    email = "nono",

                    // TODO.. Enter the merchant id here
                    merchant_id = "4321"
                }
            });

            return transactionList;
        }

        private string GetRandomInvoiceNumber()
        {
            return new Random().Next(999999999).ToString();
        }
    }
}
