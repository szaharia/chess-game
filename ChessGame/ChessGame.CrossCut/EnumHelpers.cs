using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ChessGame.CrossCut
{
    public static class EnumHelpers
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }

        public static T GetEnumByDisplayName<T>(string description, T defaultValue = default)
    where T : Enum
        {
            if (!typeof(T).IsEnum || String.IsNullOrWhiteSpace(description))
            {
                return defaultValue;
            }

            // get the enum type
            var enumType = typeof(T);

            // get the value type
            var enumValues = Enum.GetValues(enumType);
            foreach (var enumValue in enumValues)
            {
                if (((T)enumValue).GetDisplayName().Equals(description, StringComparison.InvariantCultureIgnoreCase))
                {
                    // try to change type
                    var castValue = Convert.ChangeType(enumValue, enumType);
                    // if not null, return it
                    if (castValue != null)
                    {
                        return (T)castValue;
                    }
                }
            }

            try
            {
                return (T)Enum.Parse(typeof(T), description, true);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
