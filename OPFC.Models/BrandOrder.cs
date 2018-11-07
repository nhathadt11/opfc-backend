using System;
using System.Collections.Generic;

namespace OPFC.Models
{
    public class BrandOrder
    {
        public long OrderNo { get; set; }
        public long EventNo { get; set; }
        public string EventName { get; set; }
        public string EventTypeName { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string EventStatus { get; set; }
        public string OrderStatus { get; set; }
        public int ServingNumber { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string Address { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountEarned { get; set; }
        public string PaypalRef { get; set; }
        
        public List<BrandOrderLine> BrandOderLineList { get; set; }
    }
}
