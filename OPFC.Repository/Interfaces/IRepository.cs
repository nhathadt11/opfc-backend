using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPFC.Repositories.Interfaces
{
    /// <summary>
    /// The Generic Repository Interface
    /// </summary>
    /// <typeparam name="T">Generic object type</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get all object
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">The object identifier</param>
        /// <returns></returns>
        T GetById(long id);

        /// <summary>
        /// Add object
        /// </summary>
        /// <param name="entity">The object</param>
        void Add(T entity);

        /// <summary>
        /// Update object
        /// </summary>
        /// <param name="entity">The object</param>
        void Update(T entity);

        /// <summary>
        /// Delete object
        /// </summary>
        /// <param name="entity">The object.</param>
        void Delete(T entity);

        /// <summary>
        /// Delete object by id
        /// </summary>
        /// <param name="id">The object identifier</param>
        void Delete(long id);
    }
}
