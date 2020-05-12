using System;
using System.Collections.Generic;
using System.Text;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;

namespace VehicleTrackingSystem.DataObjects.Profiles
{
    public interface IErrorMapper
    {
        ServerResponse MapToError(ServerResponse response, string errorDetail);
        UserViewModel MapToError(UserViewModel model,ServerResponse response, string errorDetail);
    }
}
