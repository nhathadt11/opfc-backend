using System;
using System.Collections.Generic;

namespace OPFC.API.DTO
{
    public class TransactionDTO
    {
        public long TransactionId { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalCommission { get; set; }

        public DateTime TransactionTime { get; set; }

        public long OrderId { get; set; }

        public long BrandId { get; set; }

        public List<TransactionDetailDTO> TransactionDetailList { get; set; }
    }
}
