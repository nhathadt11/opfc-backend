using System;
using OPFC.Models;
using OPFC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace OPFC.Repositories.Implementations
{
    public class TransactionRepository : EFRepository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(DbContext dbContext) : base(dbContext){}

        public Transaction CreateTransaction(Transaction transaction)
        {
            return DbSet.Add(transaction).Entity;
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            return DbSet.Update(transaction).Entity;
        }
    }
}
