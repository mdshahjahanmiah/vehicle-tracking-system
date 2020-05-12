using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VehicleTrackingSystem.Entities
{
    public class UserType
    {
        [Key]
        public int UserTypeId { get; set; }
        public string Name { get; set; }
    }
}
