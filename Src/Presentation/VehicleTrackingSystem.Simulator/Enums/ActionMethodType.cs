using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingSystem.Utilities.EnumAbstration;

namespace VehicleTrackingSystem.Simulator.Enums
{
    public sealed class ActionMethodType : StringEnum
    {
        public ActionMethodType(string value) : base(value)
        {
        }
        private const string DoLoginConst = "DoLogin";
        private const string RegisterConst = "Register";
        private const string RegisterVehicleConst = "RegisterVehicle";
        private const string GetAllVehiclesConst = "GetAllVehicles";
        private const string GetAllVehiclesWithDevicesConst = "GetAllVehiclesWithDevices";
        private const string RecordLocationConst = "RecordLocation";
        private const string GetAllLocationsConst = "GetAllLocations";
        private const string GetCurrentLocationConst = "GetCurrentLocation";
        public static readonly ActionMethodType DoLogin = new ActionMethodType(DoLoginConst);
        public static readonly ActionMethodType Register = new ActionMethodType(RegisterConst);
        public static readonly ActionMethodType RegisterVehicle = new ActionMethodType(RegisterVehicleConst);
        public static readonly ActionMethodType GetAllVehicles = new ActionMethodType(GetAllVehiclesConst);
        public static readonly ActionMethodType GetAllVehiclesWithDevices = new ActionMethodType(GetAllVehiclesWithDevicesConst);
        public static readonly ActionMethodType RecordLocation = new ActionMethodType(RecordLocationConst);
        public static readonly ActionMethodType GetAllLocations = new ActionMethodType(GetAllLocationsConst);
        public static readonly ActionMethodType GetCurrentLocation = new ActionMethodType(GetCurrentLocationConst);
    }
}
