using System;
using System.ComponentModel.DataAnnotations;
namespace VehicleTrackingSystem.Entities
{
    public class Vehicle
    {
        [Key]
        public int VehicleId { get; set; }
        public string Name { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string BodyType { get; set; }
        public int UserId { get; set; }
        public User Users { get; set; }
        public int DeviceId { get; set; }        
        public Device Device { get; set; }
    }
}
