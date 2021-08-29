using System;
using System.Linq;

namespace WebAPI.Extensions
{
    public static class EnumerationExtension
    {
        public static string Description(this Enum value)
        {
            dynamic displayAttribute = value.GetAttributes();
            return displayAttribute?.Description ?? string.Empty;
        }

        public static string Name(this Enum value)
        {
            dynamic displayAttribute = value.GetAttributes();
            return displayAttribute?.Name ?? string.Empty;
        }

        private static dynamic GetAttributes(this Enum value)
        {
            // get attributes  
            var field = value.GetType().GetField(value.ToString());
            var attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }

            return displayAttribute;
        }
    }
}
