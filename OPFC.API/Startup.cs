using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OPFC.API.ServiceModel.Tasks;
using OPFC.Repositories.UnitOfWork;
using OPFC.Services.Implementations;
using OPFC.Services.Interfaces;
using OPFC.Services.UnitOfWork;
using ServiceStack;
using Swashbuckle.AspNetCore.Swagger;
using System.Text;

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
                c.SwaggerDoc("v1", new Info { Title = "OPFC API", Version = "v1" });
                c.MapType<System.Int64>(() => new Schema { Type = "long" });
                c.MapType<System.Int32>(() => new Schema { Type = "int" });
            });

            services.AddMvc();
            // Config connection string here
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //                             .SetBasePath(System.AppDomain.CurrentDomain.BaseDirectory)
            //                             .AddJsonFile("appsettings.json")
            //                             .Build();

            //// configure strongly typed settings objects
            //var appSettingsSection = Configuration.GetSection("AppSettings");

            //services.Configure<OPFC.Constants.AppSettings.Secret>(appSettingsSection);

            // configure jwt authentication
            //var appSettings = appSettingsSection.Get<OPFC.Constants.AppSettings.Secret>();
            var key = Encoding.ASCII.GetBytes(OPFC.Constants.AppSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
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

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
