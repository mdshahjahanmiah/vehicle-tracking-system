using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTrackingSystem.Utilities.ExternalServices
{
    public class Locality
    {
        public string display_name { get; set; }
        public Address address { get; set; }
    }
    public class Address
    {
        public string country { get; set; }
        public string country_code { get; set; }
    }
}
