using Microsoft.EntityFrameworkCore.Storage;
using OPFC.Repositories.Interfaces;
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

        // Our OPFC Repositories here

        /// <summary>
        /// The user Repository
        /// </summary>
        IUserRepository UserRepository { get; }

        IBrandRepository BrandRepository { get; }

<<<<<<< HEAD
=======
        IPhotoRepository PhotoRepository { get; }
>>>>>>> fd8e09c0a6dbfa4f32dd39c52c01b410e8c62501
        IMealRepository MealRepository { get; }

        IMenuRepository MenuRepository { get; }

<<<<<<< HEAD
        ITransactionRepository TransactionRepository { get; }

        ITransactionDetailRepository TransactionDetailRepository { get; }

        IOrderRepository OrderRepository { get; }

=======
>>>>>>> fd8e09c0a6dbfa4f32dd39c52c01b410e8c62501
        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();
    }
}
