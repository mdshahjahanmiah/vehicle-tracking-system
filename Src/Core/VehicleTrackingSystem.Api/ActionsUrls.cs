using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Api
{
    public static class ActionsUrls
    {
        public const string Login = "DoLogin";
        public const string Registration = "Register";
        public const string RegisterVehicle = "RegisterVehicle";
        public const string GetAllVehicles = "GetAllVehicles";
        public const string GetAllVehiclesWithDevices = "GetAllVehiclesWithDevices";
        public const string RecordLocation = "RecordLocation";
        public const string GetCurrentLocation = "GetCurrentLocation";
        public const string GetAllLocations = "GetAllLocations";
    }
    public static class ServiceConsumesType
    {
        public const string Json = "application/json";
        public const string Xml = "application/xml";
    }
}
