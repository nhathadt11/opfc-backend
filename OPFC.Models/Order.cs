using System;
using OPFC.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OPFC.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderId { get; set; }

        public long UserId { get; set; }

        public DateTime DateOrdered { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } 

        public bool IsDeleted { get; set; }

        [ForeignKey("OrderId")]
        public List<Transaction> TransactionList { get; set; }

    }
}
