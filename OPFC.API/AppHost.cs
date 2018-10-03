using Funq;
using OPFC.API.ServiceModel.Tasks;
using OPFC.API.Services.Implementations;
using OPFC.Repositories.Factory;
using OPFC.Repositories.Providers;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Implementations;
using OPFC.Services.Interfaces;
using ServiceStack;

namespace OPFC.API
{
    /// <summary>
    /// The service stack apphost
    /// </summary>
    public class AppHost : AppHostBase
    {
        // Initializes your AppHost Instance, with the Service Name and assembly containing the Services
        public AppHost() : base("OPFC API SERVICE", typeof(BrandAPIService).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Funq.Container container)
        {
            //Register Redis Client Manager singleton in ServiceStack's built-in Func IOC
            container.Register(s => new RepositoryFactories());
            container.Register<IRepositoryProvider>(c => new RepositoryProvider(c.TryResolve<RepositoryFactories>())).ReusedWithin(ReuseScope.None);
            container.Register<IOpfcUow>(c => new OpfcUow(c.TryResolve<IRepositoryProvider>())).ReusedWithin(ReuseScope.None);
            container.Register<ITask>(t => new CompositeTask(new AutoMapperConfigTask()));
            container.Resolve<ITask>().Execute();

            container.Register<IOrderService>(c => new OrderService(c.TryResolve<IOpfcUow>())).ReusedWithin(ReuseScope.None);
            container.Register<IUserService>(c => new UserService(c.TryResolve<IOpfcUow>())).ReusedWithin(ReuseScope.None);
            container.Register<IUserRoleService>(c => new UserRoleService(c.TryResolve<IOpfcUow>())).ReusedWithin(ReuseScope.None);
            container.Register<IBrandService>(c => new BrandService(c.TryResolve<IOpfcUow>())).ReusedWithin(ReuseScope.None);
            container.Register<IEventAddressService>(c => new EventAddressService(c.TryResolve<IOpfcUow>())).ReusedWithin(ReuseScope.None);
        }
    }
}
