using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleTrackingSystem.Entities
{
    public class Device
    {
        [Key]
        public int DeviceId { get; set; }
        public Guid ImeiNumber { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        
    }
}
