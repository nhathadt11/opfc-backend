using System.Collections.Generic;
using System.Text;
using OPFC.Services.Interfaces;
using OPFC.Services.Providers;

namespace OPFC.Services.UnitOfWork
{
    public class ServiceUow : IServiceUow
    {
        protected IServiceProvider ServiceProvider { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ServiceUow(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
        public IPaypalService PaypalService { get { return GetRepoService<IPaypalService>(); } }
        public IServiceLocationService ServiceLocationService { get { return GetRepoService<IServiceLocationService>(); } }
        public ICityService CityService { get { return GetRepoService<ICityService>(); } }
        public IDistrictService DistrictService { get { return GetRepoService<IDistrictService>(); } }
        public IBrandService BrandService { get { return GetRepoService<IBrandService>(); } }
        public IUserService UserService { get { return GetRepoService<IUserService>(); } }
        public IEventService EventService { get { return GetRepoService<IEventService>(); } }
        public IMealService MealService { get { return GetRepoService<IMealService>(); } }
        public IMenuService MenuService { get { return GetRepoService<IMenuService>(); } }
        public IOrderService OrderService { get { return GetRepoService<IOrderService>(); } }
        public IBookMarkService BookMarkService { get { return GetRepoService<IBookMarkService>(); } }
        public IEventTypeService EventTypeService { get { return GetRepoService<IEventTypeService>(); } }
        public IPrivateRatingService PrivateRatingService { get { return GetRepoService<IPrivateRatingService>(); } }
        public IRatingService RatingService { get { return GetRepoService<IRatingService>(); } }
        public ICategoryService CategoryService { get { return GetRepoService<ICategoryService>(); } }

        private T GetRepoService<T>() where T : class
        {
            return ServiceProvider.GetService<T>();
        }
    }
}
