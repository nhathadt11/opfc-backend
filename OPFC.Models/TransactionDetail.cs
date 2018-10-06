using System;
using OPFC.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace OPFC.Models
{
    public class TransactionDetail
    {
        [Key]
        public long TransactionLineDetailId { get; set; }

        public long TransactionId { get; set; }

        public long MenuId { get; set; }

        public decimal Amount { get; set; }


    }
}
