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

        IRatingRepository RatingRepository { get; }

        IBrandRepository BrandRepository { get; }

        IPhotoRepository PhotoRepository { get; }

        IMealRepository MealRepository { get; }

        IMenuRepository MenuRepository { get; }

        IBookMarkRepository BookMarkRepository { get; }

        ITransactionRepository TransactionRepository { get; }

        ITransactionDetailRepository TransactionDetailRepository { get; }

        IOrderRepository OrderRepository { get; }

        IEventRepository EventRepository { get; }

        IEventTypeRepository EventTypeRepository { get; }

        /// <summary>
        /// Dispose
        /// </summary>
        void Dispose();
    }
}
