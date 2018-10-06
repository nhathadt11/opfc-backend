using Funq;
using OPFC.API.ServiceModel.Tasks;
using OPFC.API.Services.Implementations;
using OPFC.Repositories.Factory;
using OPFC.Repositories.Providers;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Factory;
using OPFC.Services.Implementations;
using OPFC.Services.Interfaces;
using OPFC.Services.Providers;
using OPFC.Services.UnitOfWork;
using ServiceStack;

namespace OPFC.API
{
    /// <summary>
    /// The service stack apphost
    /// </summary>
    public class AppHost : AppHostBase
    {
        // Initializes your AppHost Instance, with the Service Name and assembly containing the Services
        public AppHost() : base("OPFC API SERVICE", typeof(BrandService).Assembly) { }

        // Configure your AppHost with the necessary configuration and dependencies your App needs
        public override void Configure(Funq.Container container)
        {
            //Register Redis Client Manager singleton in ServiceStack's built-in Func IOC
            container.Register(s => new RepositoryFactories());
            container.Register<IRepositoryProvider>(c => new RepositoryProvider(c.TryResolve<RepositoryFactories>())).ReusedWithin(ReuseScope.None);
            container.Register<IOpfcUow>(c => new OpfcUow(c.TryResolve<IRepositoryProvider>())).ReusedWithin(ReuseScope.None);

            container.Register(s => new ServiceFactories(s.Resolve<IOpfcUow>())).ReusedWithin(ReuseScope.None);
            container.Register<IServiceProvider>(c => new ServiceProvider(c.TryResolve<ServiceFactories>())).ReusedWithin(ReuseScope.None);
            container.Register<IServiceUow>(c => new ServiceUow(c.TryResolve<IServiceProvider>())).ReusedWithin(ReuseScope.None);

            container.Register<ITask>(t => new CompositeTask(new AutoMapperConfigTask()));
            container.Resolve<ITask>().Execute();
        }
    }
}
