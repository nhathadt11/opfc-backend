using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OPFC.API.ServiceModel.Tasks;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.UnitOfWork;
using ServiceStack;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddCors();
            // OPFC unit of work
            services.AddSingleton<IOpfcUow, OpfcUow>();
            services.AddSingleton<IServiceUow, ServiceUow>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

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

            app.UseCors(builder => builder.WithOrigins("*")
                                          .AllowAnyHeader()
                                          .AllowAnyMethod());
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "OPFC API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseMvc();
        }
    }
}
