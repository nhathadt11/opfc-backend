using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.UnitOfWork
{
    /// <summary>
    /// The OPFC system Unit Of Work
    /// </summary>
    public interface IOpfcUow
    {
        /// <summary>
        /// Open transaction
        /// </summary>
        /// <returns></returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// Roll back if exception
        /// </summary>
        void Rollback();

        /// <summary>
        /// Commit Transaction
        /// </summary>
        /// <param name="dbContextTransaction">The db context transaction</param>
        void CommitTransaction(IDbContextTransaction dbContextTransaction);

        /// <summary>
        /// Rollback transaction
        /// </summary>
        /// <param name="dbContextTransaction">The db context transaction</param>
        void RollbackTransaction(IDbContextTransaction dbContextTransaction);

        /// <summary>
        /// Save pending changes to the data store.
        /// </summary>
        void Commit();

        // Repositories
        // I..Repository I..Repository {get; }

        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();
    }
}
