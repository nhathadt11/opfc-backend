using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OPFC.Repositories.UnitOfWork;
using ServiceStack;

namespace OPFC.API
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // OPFC unit of work
            services.AddSingleton<IOpfcUow, OpfcUow>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Register ServiceStack AppHost as a .NET Core module
            app.UseServiceStack(new AppHost
            {
                // Use **appsettings.json** and config sources
                AppSettings = new NetCoreAppSettings(Configuration)
            });

            app.UseMvc();
        }
    }
}
