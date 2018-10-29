using System.Collections.Generic;
using OPFC.Models;

namespace OPFC.API.DTO
{
    public class EventPlannerOrderLineDTO
    {
        public long OrderLineId { get; set; }
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public string BrandName { get; set; }
        public string ImageUrl { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
        public decimal OtherFee { get; set; }
        public string Status { get; set; }
        public string PaypalSaleRef { get; set; }
        public List<IdNameValue> MealList { get; set; }
    }
}
