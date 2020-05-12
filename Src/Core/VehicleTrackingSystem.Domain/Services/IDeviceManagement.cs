using System;
using System.Collections.Generic;
using System.Text;
using VehicleTrackingSystem.DataObjects.Domain;

namespace VehicleTrackingSystem.Domain.Services
{
    public interface IDeviceManagement
    {
        void AddDevice(DeviceViewModel entity);
        List<DeviceViewModel> GetAllDevices();
        bool DeviceExists(Guid number);
    }
}
