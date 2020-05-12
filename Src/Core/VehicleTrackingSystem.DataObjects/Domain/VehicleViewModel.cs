using System;
using System.Collections.Generic;
using System.Text;
using VehicleTrackingSystem.DataObjects.Abstraction;
using VehicleTrackingSystem.Entities;

namespace VehicleTrackingSystem.DataObjects.Domain
{
    public class VehicleViewModel : ServerResponse
    {
        public int VehicleId { get; set; }
        public string Name { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string BodyType { get; set; }
        public int UserId { get; set; }
        public UserViewModel User { get; set; }
        public int DeviceId { get; set; }
        public DeviceViewModel Device { get; set; }
    }
}
