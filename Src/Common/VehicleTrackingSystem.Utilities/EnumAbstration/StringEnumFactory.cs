using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace VehicleTrackingSystem.Utilities.EnumAbstration
{
    public class StringEnumFactory
    {
        internal StringEnumFactory()
        {
        }

        private static readonly Dictionary<Type, StringEnum[]> PropertyValues =
            new Dictionary<Type, StringEnum[]>();

        public string[] GetPossibleValues(Type type) => GetProperties(type).Select(x => x.Value).ToArray();

        private StringEnum[] GetProperties(Type type)
        {
            if (!PropertyValues.ContainsKey(type))
                PropertyValues.Add(type, GetValuesRange(type));
            return PropertyValues[type];
        }

        public T CreateFromValue<T>(string value) where T : StringEnum
        {
            var type = typeof(T);
            var properties = GetProperties(type);
            var result = properties.FirstOrDefault(x => x.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
            if (result == null)
                throw new ArgumentOutOfRangeException(nameof(value));
            return (T)result;
        }

        public T CreateFromName<T>(string name) where T : StringEnum
        {
            var type = typeof(T);
            var properties = GetProperties(type);
            if (!properties.Select(x => x.Name).Contains(name))
                throw new ArgumentOutOfRangeException(nameof(name));
            return (T)properties.First(x => x.Name == name);
        }

        private StringEnum[] GetValuesRange(Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(x => x.FieldType == type)
                .Select(x => x.GetValue(null) as StringEnum)
                .ToArray();
        }
    }
}
