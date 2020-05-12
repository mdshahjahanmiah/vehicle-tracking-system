using System;
using System.Collections.Generic;
using System.Text;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;

namespace VehicleTrackingSystem.Domain.Services
{
    public interface IVehicleManagement
    {
        ServerResponse AddVehicle(VehicleViewModel entity);
        List<VehicleViewModel> GetAllVehicles();
        List<VehicleViewModel> GetAllVehiclesWithDevices();
        VehicleViewModel GetUserByVehicle(int vehicleId);
        bool IsVehicleExists(int vehicleId);
    }
}
