using System.ComponentModel.DataAnnotations;

namespace OPFC.Models
{
    public class OrderLineDetail
    {
        [Key]
        public long Id { get; set; }
        public long OrderLineId { get; set; }
        public long MenuId { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
    }
}
