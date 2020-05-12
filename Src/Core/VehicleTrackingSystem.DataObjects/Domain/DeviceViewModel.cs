using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTrackingSystem.DataObjects.Domain
{
    public class DeviceViewModel
    {
        public int DeviceId { get; set; }
        public Guid ImeiNumber { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
