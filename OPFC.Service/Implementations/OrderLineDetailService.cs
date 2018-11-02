using System;
using System.Collections.Generic;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;

namespace OPFC.Services.Implementations
{
    public class OrderLineDetailService : IOrderLineDetailService
    {
        private readonly IOpfcUow _opfcUow;

        public OrderLineDetailService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public void CreateRange(List<OrderLineDetail> orderLineDetails)
        {
            _opfcUow.OrderLineDetailRepository.CreateRange(orderLineDetails);
        }
    }
}
