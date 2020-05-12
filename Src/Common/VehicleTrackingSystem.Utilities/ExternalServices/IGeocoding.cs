using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Utilities.ExternalServices
{
    public interface IGeocoding
    {
        string GetLocality(double latitude, double longitude);
    }
}
