using System;
using System.Collections.Generic;

namespace OPFC.Models
{
    public class EventPlannerOrder
    {
        public long OrderNo { get; set; }
        public long EventNo { get; set; }
        public string EventName { get; set; }
        public string EventTypeName { get; set; }
        public DateTime OrderAt { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string OrderStatus { get; set; }
        public int ServingNumber { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaypalRef { get; set; }
        public int MenuNumber { get; set; }
        public string Note { get; set; }
        
        public List<EventPlannerOrderLine> OrderLineList { get; set; }
    }
}
