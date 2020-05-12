using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleTrackingSystem.Entities
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude{get;set;}
        public double Altitude { get; set; }
        public double HorizontalAccuracy { get; set; }
        public double VerticalAccuracy { get; set; }
        public double Speed { get; set; }
        public DateTime CreatedTime { get; set; }
        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
