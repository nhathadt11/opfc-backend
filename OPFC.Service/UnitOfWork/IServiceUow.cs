using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.UnitOfWork
{
    public interface IServiceUow
    {
        IServiceLocationService ServiceLocationService { get; }
        ICityService CityService { get; }
        IDistrictService DistrictService { get; }
        IRatingService RatingService { get; }
        IBrandService BrandService { get; }
        IUserService UserService { get; }
        IMealService MealService { get; }
        IMenuService MenuService { get; }
        IOrderService OrderService { get; }
        IBookMarkService BookMarkService { get; }
        IEventService EventService { get; }
        IEventTypeService EventTypeService { get; }
        ICategoryService CategoryService { get; }
        IPrivateRatingService PrivateRatingService { get; }
    }
}
