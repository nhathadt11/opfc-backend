using System;
using OPFC.Models;
namespace OPFC.Repositories.Interfaces
{
    public interface ITransactionDetailRepository
    {
        TransactionDetail CreateTransactionDetail(TransactionDetail transactionDetail);

        TransactionDetail UpdateTransactionDetail(TransactionDetail transactionDetail);

    }
}
