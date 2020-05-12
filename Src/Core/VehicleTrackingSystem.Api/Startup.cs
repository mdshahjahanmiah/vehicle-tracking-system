using System.Text;
using System.Threading;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using VehicleTrackingSystem.DataAccess.Repository;
using VehicleTrackingSystem.DataObjects.ApiSettings;
using VehicleTrackingSystem.DataObjects.Profiles;
using VehicleTrackingSystem.Domain.Services;
using VehicleTrackingSystem.Security.Handlers;
using VehicleTrackingSystem.Utilities.ExternalServices;
using VehicleTrackingSystem.Validation.Validators;

namespace VehicleTrackingSystem.Api
{
    public partial class Startup
    {
        private readonly ILoggerFactory _loggerFactory;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            ExtendThreadPool();
            SetupCrossOriginResourceSharing(services);
            var settings = GetAppConfigurationSection();
            AddVehicleTrackingSystemDependencies(services, settings);
            AutoMapperConfigurations(services);
            ConfigureJwtAuthentication(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            ConfigureTransientServices(services);
            ConfigureSingletonServices(services);
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();            
            else app.UseHsts();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
        private static void AutoMapperConfigurations(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
        private static void SetupCrossOriginResourceSharing(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("AllowAllHeaders", builder => builder
                                .AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod()));
            services.AddHealthChecks();
        }
        private void ExtendThreadPool()
        {
            ThreadPool.GetMinThreads(out var test, out var minIoc);
            ThreadPool.SetMinThreads(Configuration.GetValue<int>("MinWorkerThreads"), minIoc);
        }
        private void ConfigureJwtAuthentication(IServiceCollection services)
        {
            var key = Encoding.ASCII.GetBytes(GetAppConfigurationSection().Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
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
            IdentityModelEventSource.ShowPII = true;
        }
        private AppSettings GetAppConfigurationSection() => Configuration.GetSection("AppSettings").Get<AppSettings>();
        private void ConfigureSingletonServices(IServiceCollection services)
        {
            services.AddSingleton(GetAppConfigurationSection());
        }
        private void ConfigureTransientServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IUserManagement), typeof(UserManagement));
            services.AddTransient(typeof(IVehicleManagement), typeof(VehicleManagement));
            services.AddTransient(typeof(ILocationManagement), typeof(LocationManagement));
            services.AddTransient(typeof(IDeviceManagement), typeof(DeviceManagement));
            services.AddTransient(typeof(IRequestValidator), typeof(RequestValidator));
            services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddTransient(typeof(ICryptographyHandler), typeof(CryptographyHandler));
            services.AddTransient(typeof(IJwtTokenHandler), typeof(JwtTokenHandler));
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IGeocoding), typeof(GeocodingHandler));
            services.AddTransient(typeof(IErrorMapper), typeof(ErrorMappingProfile));
        }
    }
}
