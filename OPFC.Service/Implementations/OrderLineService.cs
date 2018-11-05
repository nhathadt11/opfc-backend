using System;
using System.Collections.Generic;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;

namespace OPFC.Services.Implementations
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IOpfcUow _opfcUow;

        public OrderLineService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public List<OrderLine> GetAllByOrderId(long orderId)
        {
            return _opfcUow.OrderLineRepository.GetAllByOrderId(orderId);
        }

        public OrderLine GetOrderLineById(long id)
        {
            return _opfcUow.OrderLineRepository.GetOrderLineById(id);
        }
    }
}
