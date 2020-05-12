using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VehicleTrackingSystem.DataAccess.DbContext;
using VehicleTrackingSystem.DataObjects.ApiSettings;

namespace VehicleTrackingSystem.Api
{
    public partial class Startup
    {
        private void AddVehicleTrackingSystemDependencies(IServiceCollection services, AppSettings settings)
        {
            if (settings.InMemoryDatabase)
            {
                services.AddDbContext<VehicleTrackingCommandsContext>(options => options.UseInMemoryDatabase("VehicleTrackingContext"));
                services.AddDbContext<VehicleTrackingQueriesContext>(options => options.UseInMemoryDatabase("VehicleTrackingContext"));
            }
            else
            {
                services.AddDbContextPool<VehicleTrackingCommandsContext>(options => options.UseSqlServer(settings.ConnectionStrings.SqlServer.Commands));
                services.AddDbContextPool<VehicleTrackingQueriesContext>(options => options.UseSqlServer(settings.ConnectionStrings.SqlServer.Queries));
            }
            
        }
    }
}
