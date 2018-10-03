using System;
using System.Collections.Generic;
namespace OPFC.API.DTO
{
    public class OrderDTO
    {
        public long OrderId { get; set; }

        public long UserId { get; set; }

        public DateTime DateOrdered { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; }

        public List<TransactionDTO> TransactionList { get; set; }
    }
}
