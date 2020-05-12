using System;
using System.Collections.Generic;
using System.Text;
using VehicleTrackingSystem.Utilities.EnumAbstration;

namespace VehicleTrackingSystem.Validation.Enums
{
    public class UserTypes : StringEnum
    {
        public UserTypes(string value) : base(value)
        {
        }
        public const int Admin = 1;
        public const int User = 2;
    }
}
