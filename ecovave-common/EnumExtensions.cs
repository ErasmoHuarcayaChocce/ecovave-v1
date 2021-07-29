using System;
using System.Collections.Generic;

namespace ecovave.common
{
    public static class EnumExtensions
    {
        public static List<EnumValue> GetValues<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(new EnumValue()
                {
                    name = Enum.GetName(typeof(T), itemType),
                    value = (int) itemType
                });
            }

            return  values;
        }
    }
}