using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace OPFC.Repositories.Implementations
{
    public class TransactionDetailRepository : EFRepository<TransactionDetail>, ITransactionDetailRepository
    {
        public TransactionDetailRepository(DbContext dbContext) :base(dbContext){ }

        public TransactionDetail CreateTransactionDetail(TransactionDetail transactionDetail)
        {
            return DbSet.Add(transactionDetail).Entity;
        }

        public TransactionDetail UpdateTransactionDetail(TransactionDetail transactionDetail)
        {
            return DbSet.Update(transactionDetail).Entity;
        }
    }
}
