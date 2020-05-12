using System;
using System.Collections.Generic;
using System.Text;
using VehicleTrackingSystem.DataObjects.Abstraction;

namespace VehicleTrackingSystem.DataObjects.Domain
{
    public class UserViewModel : ServerResponse
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int UserTypeId { get; set; }
    }
}
