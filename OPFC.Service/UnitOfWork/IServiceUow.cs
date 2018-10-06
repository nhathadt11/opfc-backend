using OPFC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OPFC.Services.UnitOfWork
{
    public interface IServiceUow
    {
        IBrandService BrandService { get; }
        IUserService UserService { get; }
        IEventService EventService { get; }
        IMealService MealService { get; }
        IMenuService MenuService { get; }
        IOrderService OrderService { get; }
        IBookMarkService BookMarkService { get; }
    }
}
