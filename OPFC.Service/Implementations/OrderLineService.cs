using System;
using System.Collections.Generic;
using System.Linq;
using OPFC.FirebaseService;
using OPFC.Models;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Interfaces;
using OPFC.Services.UnitOfWork;

namespace OPFC.Services.Implementations
{
    public class OrderLineService : IOrderLineService
    {
        private readonly IServiceUow _serviceUow = ServiceStack.ServiceStackHost.Instance.TryResolve<IServiceUow>();
        private readonly IOpfcUow _opfcUow;

        public OrderLineService(IOpfcUow opfcUow)
        {
            _opfcUow = opfcUow;
        }

        public void Approve(long orderLineId)
        {
            ChangeOrderLineStatus(orderLineId, OrderStatus.Approved);

            OrderPayload orderPayload = _serviceUow.OrderService.GetOrderPayloadByOrderLineId(orderLineId);
            orderPayload.Verb = "approved";
            
            FirebaseService.FirebaseService.Instance.SendNotification(orderPayload);
        }

        public void Cancel(long orderLineId)
        {
            ChangeOrderLineStatus(orderLineId, OrderStatus.Canceled);

            OrderPayload orderPayload = _serviceUow.OrderService.GetOrderPayloadByOrderLineId(orderLineId);
            orderPayload.Verb = "canceled";
            
            FirebaseService.FirebaseService.Instance.SendNotification(orderPayload);
        }
        
        public void MarkAsCompleted(long orderLineId)
        {
            ChangeOrderLineStatus(orderLineId, OrderStatus.Completed);


            OrderPayload orderPayload = _serviceUow.OrderService.GetEventPlannerOrderPayloadByOrderLineId(orderLineId);
            orderPayload.Verb = "marked as completed";
            
            FirebaseService.FirebaseService.Instance.SendNotification(orderPayload);
        }

        public bool Exists(long orderLineId)
        {
            return _opfcUow.OrderLineRepository.GetById(orderLineId) != null;
        }

        public List<OrderLine> GetAllByOrderId(long orderId)
        {
            return _opfcUow.OrderLineRepository.GetAllByOrderId(orderId);
        }

        public OrderLine GetOrderLineById(long id)
        {
            return _opfcUow.OrderLineRepository.GetOrderLineById(id);
        }
        
        void ChangeOrderLineStatus(long orderLineId, OrderStatus status)
        {
            OrderLine orderLine = _opfcUow.OrderLineRepository.GetById(orderLineId);
            orderLine.Status = (int)status;
            _opfcUow.OrderLineRepository.Update(orderLine);
            _opfcUow.Commit();
        }
    }
}
