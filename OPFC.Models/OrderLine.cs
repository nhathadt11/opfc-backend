using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPFC.Models
{
    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long BrandId { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountEarned { get; set; }
        public int Status { get; set; }
    }
}