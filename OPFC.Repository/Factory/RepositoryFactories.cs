using Microsoft.EntityFrameworkCore;
using OPFC.Repositories.Implementations;
using OPFC.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Repositories.Factory
{
    public class RepositoryFactories
    {
        /// <summary>
        /// Return the runtime OPFC SERVER repository factory functions,
        /// each one is a factory for a repository of a particular type.
        /// </summary>
        /// <remarks>
        /// MODIFY THIS METHOD TO ADD CUSTOM OPFC SERVER FACTORY FUNCTIONS
        /// </remarks>
        private IDictionary<Type, Func<DbContext, object>> GetOPFCFactories()
        {
            // TODO: Register repository here
            return new Dictionary<Type, Func<DbContext, object>>
                {
                {typeof(ICityRepository), dbContext => new CityRepository(dbContext)},
                {typeof(IDistrictRepository), dbContext => new DistrictRepository(dbContext)},
                {typeof(IServiceLocationRepository), dbContext => new ServiceLocationRepository(dbContext)},
                {typeof(IRatingRepository), dbContext => new RatingRepository(dbContext)},
                {typeof(IOrderRepository), dbContext => new OrderRepository(dbContext)},
                {typeof(IMenuRepository), dbContext => new MenuRepository(dbContext)},
                {typeof(IBookMarkRepository), dbContext => new BookMarkRepository(dbContext)},
                {typeof(IUserRepository), dbContext => new UserRepository(dbContext)},
                {typeof(IUserRoleRepository), dbContext => new UserRoleRepository(dbContext)},
                {typeof(IBrandRepository), dbContext => new BrandRepository(dbContext)},
                {typeof(IEventAddressRepository), dbContext => new EventAddressRepository(dbContext)},
                {typeof(IMealRepository), dbContext => new MealRepository(dbContext)},
                {typeof(IPhotoRepository), dbContext => new PhotoRepository(dbContext)},
                {typeof(IEventRepository), dbContext => new EventRepository(dbContext)},
                {typeof(IEventTypeRepository), dbContext => new EventTypeRepository(dbContext)},
                {typeof(IMenuMealRepository), dbContext => new MenuMealRepository(dbContext)},
                {typeof(IOrderLineRepository), dbContext => new OrderLineRepository(dbContext)},
            };
        }

        /// <summary>
        /// Constructor that initializes with runtime OPFC Server repository factories
        /// </summary>
        public RepositoryFactories()
        {
            _repositoryFactories = GetOPFCFactories();
        }

        /// <summary>
        /// Constructor that initializes with an arbitrary collection of factories
        /// </summary>
        /// <param name="factories">
        /// The repository factory functions for this instance. 
        /// </param>
        /// <remarks>
        /// This ctor is primarily useful for testing this class
        /// </remarks>
        public RepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories)
        {
            _repositoryFactories = factories;
        }

        /// <summary>
        /// Get the repository factory function for the type.
        /// </summary>
        /// <typeparam name="T">Type serving as the repository factory lookup key.</typeparam>
        /// <returns>The repository function if found, else null.</returns>
        /// <remarks>
        /// The type parameter, T, is typically the repository type 
        /// but could be any type (e.g., an entity type)
        /// </remarks>
        public Func<DbContext, object> GetRepositoryFactory<T>()
        {
            Func<DbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }

        /// <summary>
        /// Get the factory for <see cref="IRepository{T}"/> where T is an entity type.
        /// </summary>
        /// <typeparam name="T">The root type of the repository, typically an entity type.</typeparam>
        /// <returns>
        /// A factory that creates the <see cref="IRepository{T}"/>, given an EF <see cref="DbContext"/>.
        /// </returns>
        /// <remarks>
        /// Looks first for a custom factory in <see cref="_repositoryFactories"/>.
        /// If not, falls back to the <see cref="DefaultEntityRepositoryFactory{T}"/>.
        /// You can substitute an alternative factory for the default one by adding
        /// a repository factory for type "T" to <see cref="_repositoryFactories"/>.
        /// </remarks>
        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        /// <summary>
        /// Default factory for a <see cref="IRepository{T}"/> where T is an entity.
        /// </summary>
        /// <typeparam name="T">Type of the repository's root entity</typeparam>
        protected virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new EFRepository<T>(dbContext);
        }

        /// <summary>
        /// Get the dictionary of repository factory functions.
        /// </summary>
        /// <remarks>
        /// A dictionary key is a System.Type, typically a repository type.
        /// A value is a repository factory function
        /// that takes a <see cref="DbContext"/> argument and returns
        /// a repository object. Caller must know how to cast it.
        /// </remarks>
        private readonly IDictionary<Type, Func<DbContext, object>> _repositoryFactories;

    }
}
