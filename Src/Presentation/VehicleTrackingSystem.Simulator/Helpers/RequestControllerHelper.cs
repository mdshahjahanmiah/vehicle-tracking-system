using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingSystem.DataObjects.Domain;
using VehicleTrackingSystem.Simulator.Models;

namespace VehicleTrackingSystem.Simulator.Helpers
{
    public class RequestControllerHelper
    {
        public VehicleViewModel VehicleRequest()
        {
            var model = new VehicleViewModel
            {
                BodyType = "Car",
                Maker = "Germany",
                Model = "Cayenne",
                Name = "Porsche",
                Year = "2003",
                UserId = 1,
                Device = new DeviceViewModel()
                {
                    ImeiNumber = Guid.NewGuid(),
                    Name = "Cartrack",
                    Status = true
                }
            };
            return model;
        }
        public LocationViewModel LocationRequest()
        {
            var model = new LocationViewModel
            {
               Latitude = 13.736717,
               Longitude = 100.523186
            };
            return model;
        }
        public List<SelectItem> GetUserTypes()
        {
            var listItems = new List<SelectItem>()
            {
                new SelectItem {UserTypeId = 1, Name = "Admin"},
                new SelectItem {UserTypeId = 2, Name = "User"}
            };
            return listItems;
        }
    }
}
