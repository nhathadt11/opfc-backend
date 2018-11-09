using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string Note { get; set; }
        [NotMapped]
        public long BrandId { get; set; }
        [NotMapped]
        public string BrandName { get; set; }
        [NotMapped]
        public int StatusId { get; set; }
        [NotMapped]
        public string StatusName { get; set; }
    }
}
