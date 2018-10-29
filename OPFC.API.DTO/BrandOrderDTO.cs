using System;
using System.Collections.Generic;

namespace OPFC.API.DTO
{
    public class BrandOrderDTO
    {
        public long OrderNo { get; set; }
        public long EventNo { get; set; }
        public string EventName { get; set; }
        public string EventTypeName { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Status { get; set; }
        public int ServingNumber { get; set; }
        public string CityName { get; set; }
        public string DistrictName { get; set; }
        public string Address { get; set; }
        public decimal TotalPrice { get; set; }
        
        public List<BrandOrderLineDTO> BrandOderLineList { get; set; }
    }
}
