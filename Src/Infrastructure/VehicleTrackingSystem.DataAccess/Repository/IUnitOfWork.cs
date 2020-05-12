using System;
using VehicleTrackingSystem.DataAccess.DbContext;
namespace VehicleTrackingSystem.DataAccess.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        VehicleTrackingCommandsContext CommandsContext { get; }
        VehicleTrackingQueriesContext QueriesContext { get; }
        void Commit();
    }
}
