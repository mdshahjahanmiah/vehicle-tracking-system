using System;
using System.Collections.Generic;
using System.Text;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.DataObjects.Domain;

namespace VehicleTrackingSystem.Domain.Services
{
    public interface ILocationManagement
    {
        ServerResponse RecordLocation(LocationViewModel entity);
        List<LocationViewModel> RetriveLocation(LocationViewModel entity);
        List<LocationViewModel> GetAllLocations();
    }
}
