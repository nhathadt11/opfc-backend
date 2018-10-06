using System;
namespace OPFC.API.DTO
{
    public class TransactionDetailDTO
    {
        public long TransactionLineDetailId { get; set; }

        public long TransactionId { get; set; }

        public long MenuId { get; set; }

        public decimal Amount { get; set; }
    }
}
