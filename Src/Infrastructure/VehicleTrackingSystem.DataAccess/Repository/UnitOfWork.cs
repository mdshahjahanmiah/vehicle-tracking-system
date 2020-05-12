using VehicleTrackingSystem.DataAccess.DbContext;
namespace VehicleTrackingSystem.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public VehicleTrackingCommandsContext CommandsContext { get; }
        public VehicleTrackingQueriesContext QueriesContext { get; }
        public UnitOfWork(VehicleTrackingCommandsContext commandsContext, VehicleTrackingQueriesContext queriesContext)
        {
            CommandsContext = commandsContext;
            QueriesContext = queriesContext;
        }
        public void Commit()
        {
            CommandsContext.SaveChanges();
        }
        public void Dispose()
        {
            CommandsContext.Dispose();
            QueriesContext.Dispose();

        }
    }
}
