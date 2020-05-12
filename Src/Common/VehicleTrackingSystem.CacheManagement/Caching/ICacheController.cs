using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTrackingSystem.CacheManagement.Caching
{
    public interface ICacheController
    {
        string Get(string key);
        void Set(string key, string value);
        void Remove(string key);
    }
}
