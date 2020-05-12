using System;
using System.ComponentModel.DataAnnotations;
using VehicleTrackingSystem.DataObjects.Abstraction;

namespace VehicleTrackingSystem.DataObjects.Domain
{
    public class LocationViewModel : ServerResponse
    {
        public int LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Altitude { get; set; }
        public double HorizontalAccuracy { get; set; }
        public double VerticalAccuracy { get; set; }
        public double Speed { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime DateFrom { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd HH:mm}")]
        public DateTime DateTo { get; set; }
        public int VehicleId { get; set; }
        public string Locality { get; set; }
    }
}
