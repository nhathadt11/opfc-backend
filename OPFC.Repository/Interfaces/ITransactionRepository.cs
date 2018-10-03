using System;
using OPFC.Models;
namespace OPFC.Repositories.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Transaction CreateTransaction(Transaction transaction);

        Transaction UpdateTransaction(Transaction transaction);
    }
}
