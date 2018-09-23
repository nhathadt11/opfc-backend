using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using OPFC.Repositories.Interfaces;
using OPFC.Repositories.Providers;

namespace OPFC.Repositories.UnitOfWork
{
    /// <summary>
    /// The concrete OPFC Unit Of Work
    /// </summary>
    public class OpfcUow : IOpfcUow, IDisposable
    {
        /// <summary>
        /// The OpfcDbContext
        /// </summary>
        private OpfcDbContext OpfcDbContext { get; set; }

        protected IRepositoryProvider RepositoryProvider { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public OpfcUow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();
            repositoryProvider.DbContext = OpfcDbContext;
            RepositoryProvider = repositoryProvider;
        }

        public IUserRepository UserRepository { get { return GetRepo<IUserRepository>(); } }

        /// <summary>
        /// Create new OpfcDbContext
        /// </summary>
        protected void CreateDbContext()
        {
            OpfcDbContext = new OpfcDbContext();
        }

        /// <summary>
        /// Open transaction
        /// </summary>
        /// <returns></returns>
        public IDbContextTransaction BeginTransaction()
        {
            return OpfcDbContext.Database.BeginTransaction();
        }

        /// <summary>
        /// Save pending changes to the data store.
        /// </summary>
        public void Commit()
        {
            try
            {
                OpfcDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// Commit Transaction
        /// </summary>
        /// <param name="dbContextTransaction">The db context transaction</param>
        public void CommitTransaction(IDbContextTransaction dbContextTransaction)
        {
            dbContextTransaction.Commit();
        }

        /// <summary>
        /// Roll back if exception
        /// </summary>
        public void Rollback()
        {
            foreach (EntityEntry entry in OpfcDbContext.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Rollback transasction
        /// </summary>
        /// <param name="dbContextTransaction">The db context transaction</param>
        public void RollbackTransaction(IDbContextTransaction dbContextTransaction)
        {
            dbContextTransaction.Rollback();
        }

        private T GetRepo<T>() where T : class
        {
            return RepositoryProvider.GetRepository<T>();
        }

        #region DISPOSAL
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose with disposing flag
        /// </summary>
        /// <param name="disposing">Disposing flag</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (OpfcDbContext != null) OpfcDbContext.Dispose();
        }
        #endregion
    }
}
