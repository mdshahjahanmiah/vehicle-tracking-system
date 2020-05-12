using System;
using System.Collections.Generic;
using System.Text;

namespace VehicleTrackingSystem.Utilities.EnumAbstration
{
    public abstract class StringEnum
    {
        public static readonly StringEnumFactory Factory = new StringEnumFactory();

        protected StringEnum(string value)
        {
            Value = value;
        }

        internal StringEnum(string value, string name)
        {
            Value = value;
            Name = name;
        }

        public string Value { get; }

        public string Name { get; }

        public static implicit operator string(StringEnum e)
        {
            return e.Value;
        }

        private bool Equals(StringEnum other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((StringEnum)obj);
        }

        public static bool operator ==(StringEnum left, StringEnum right)
        {
            if (Equals(left, null) && Equals(right, null))
                return true;
            if (Equals(left, null) || Equals(right, null))
                return false;
            return left.Equals(right);
        }

        public static bool operator !=(StringEnum left, StringEnum right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
