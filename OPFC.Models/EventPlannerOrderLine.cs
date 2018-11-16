using System;
using System.Collections.Generic;

namespace OPFC.Models
{
    public class EventPlannerOrderLine
    {
        public long OrderLineId { get; set; }
        public long BrandOrderLineId { get; set; }
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public long BrandId { get; set; }
        public string BrandName { get; set; }
        public string ImageUrl { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
        public decimal OtherFee { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string PaypalSaleRef { get; set; }
        public List<IdNameValue> MealList { get; set; }
        public bool DidRate { get; set; }
    }
}
