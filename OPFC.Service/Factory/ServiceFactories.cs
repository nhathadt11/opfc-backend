using Microsoft.Extensions.Options;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Implementations;
using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.Factory
{
    public class ServiceFactories
    {
        /// <summary>
        /// Return the runtime OPFC SERVER repository factory functions,
        /// each one is a factory for a repository of a particular type.
        /// </summary>
        /// <remarks>
        /// MODIFY THIS METHOD TO ADD CUSTOM OPFC SERVER FACTORY FUNCTIONS
        /// </remarks>
        private IDictionary<Type, Func<object, object>> GetServiceFactories(IOpfcUow opfcUow)
        {
            // TODO: Register repository here
            return new Dictionary<Type, Func<object, object>>
                {
                {typeof(IBrandService), coreService => new BrandService(opfcUow)},
                {typeof(IBookMarkService), coreService => new BookMarkService(opfcUow)},
                {typeof(IEventService), coreService => new EventService(opfcUow)},
                {typeof(IMealService), coreService => new MealService(opfcUow)},
                {typeof(IMenuService), coreService => new MenuService(opfcUow)},
                {typeof(IOrderService), coreService => new OrderService(opfcUow)},
                {typeof(IUserService), coreService => new UserService(opfcUow)},
                {typeof(IEventTypeService), coreService => new EventTypeService(opfcUow)},
            };
        }

        /// <summary>
        /// Constructor that initializes with runtime OPFC Server repository factories
        /// </summary>
        public ServiceFactories(IOpfcUow opfcUow)
        {
            _serviceFactories = GetServiceFactories(opfcUow);
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
        public ServiceFactories(IDictionary<Type, Func<object, object>> factories)
        {
            _serviceFactories = factories;
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
        public Func<object, object> GetServiceFactory<T>()
        {
            Func<object, object> factory;
            _serviceFactories.TryGetValue(typeof(T), out factory);
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
        public Func<object, object> GetServiceFactoryForEntityType<T>() where T : class
        {
            return GetServiceFactory<T>() ?? DefaultServiceFactory<T>();
        }

        /// <summary>
        /// Default factory for a <see cref="IRepository{T}"/> where T is an entity.
        /// </summary>
        /// <typeparam name="T">Type of the repository's root entity</typeparam>
        protected virtual Func<object, object> DefaultServiceFactory<T>() where T : class
        {
            return service => new object();
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
        private readonly IDictionary<Type, Func<object, object>> _serviceFactories;

    }
}
