using System;
using System.Collections.Generic;
using System.Text;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;

namespace VehicleTrackingSystem.Validation.Validators
{
    public interface IRequestValidator
    {
        ServerResponse ValidateLocation(LocationViewModel request);
        ServerResponse ValidateVehicle(VehicleViewModel request);
        ServerResponse ValidateUser(UserViewModel request);
        bool DeviceExists(Guid number);
        bool UserExists(string userName);
        bool VehicleExists(int vehicleId);
        bool IsAdministrator(string token);
        string GetUserFromToken(string token);
        bool IsValidToken(string token);
        bool IsPermittedToAddLocation(string userId, int vehicleId);
        bool IsPermittedToEditVehicle(string userId, int vehicleId);
    }
}
