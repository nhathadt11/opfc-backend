using System;
using System.Collections.Generic;
using OPFC.API.DTO;
using OPFC.Models;
using ServiceStack;
namespace OPFC.API.ServiceModel.Order
{
    [Route("/Order/CreateOrder/","POST")]
    public class CreateOrderRequest : IReturn<CreateOrderResponse>
    {
        public long UserId { get; set; }
        public long EventId { get; set; }
        public List<RequestOrderItem> RequestMenuList { get; set; }
        public string PaymentId { get; set; }
        public string SaleId { get; set; }

    }

    [Route("/Order/UpdateOrder/", "POST")]
    public class UpdateOrderRequest : IReturn<UpdateOrderResponse>
    {
        public long OrderId { get; set; }

        public long UserId { get; set; }

        public DateTime DateOrdered { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public List<TransactionDTO> TransactionList { get; set; }

    }
}
