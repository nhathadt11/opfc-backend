namespace OPFC.API.DTO
{
    public class BrandOrderLineDTO
    {
        public long MenuId { get; set; }
        public string MenuName { get; set; }
        public string Note { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public string PaypalSaleRef { get; set; }
    }
}