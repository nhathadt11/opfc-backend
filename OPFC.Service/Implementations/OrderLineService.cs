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

        public void Cancel(long orderLineId)
        {
            OrderLine orderLine = _opfcUow.OrderLineRepository.GetById(orderLineId);
            orderLine.Status = (int)OrderStatus.Canceled;
            _opfcUow.OrderLineRepository.Update(orderLine);
            _opfcUow.Commit();
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
