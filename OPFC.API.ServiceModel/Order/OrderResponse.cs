using System;
using OPFC.API.DTO;
using ServiceStack;
namespace OPFC.API.ServiceModel.Order
{
    public class CreateOrderResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public OrderDTO Order { get; set; }
    }

    public class UpdateOrderResponse
    {
        public ResponseStatus ResponseStatus { get; set; }

        public OrderDTO Order { get; set; }
    }
}
