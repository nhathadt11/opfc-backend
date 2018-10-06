using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OPFC.Models
{
    public class Transaction
    {
        [Key]
        public long TransactionId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalCommission { get; set; }

        public DateTime TransactionTime { get; set; }

        public long OrderId { get; set; }

        public long BrandId { get; set; }

        [ForeignKey("TransactionId")]
        public List<TransactionDetail> TransactionDetailList { get; set; }

    }
}
