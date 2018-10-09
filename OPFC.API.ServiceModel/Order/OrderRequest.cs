using System;
using System.Collections.Generic;
using OPFC.API.DTO;
using ServiceStack;
namespace OPFC.API.ServiceModel.Order
{
    [Route("/Order/CreateOrder/","POST")]
    public class CreateOrderRequest : IReturn<CreateOrderResponse>
    {
        public OrderDTO order { get; set; }

    }

    [Route("/Order/UpdateOrder/", "POST")]
    public class UpdateOrderRequest : IReturn<UpdateOrderResponse>
    {
        public OrderDTO order { get; set; }

    }
}
