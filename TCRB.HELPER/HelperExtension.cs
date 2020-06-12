using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TCRB
{
    public static class HelperExtension
    {
        //public static bool IsEmpty<T>(this IEnumerable<T> list)
        //{
        //    if (list == null)
        //    {
        //        return true;
        //    }

        //    var asString = list as string;
        //    if (asString != null)
        //    {
        //        return string.IsNullOrWhiteSpace(asString);
        //    }

        //    var genericCollection = list as ICollection<T>;
        //    if (genericCollection != null)
        //    {
        //        return genericCollection.Count == 0;
        //    }

        //    var nonGenericCollection = list as ICollection;
        //    if (nonGenericCollection != null)
        //    {
        //        return nonGenericCollection.Count == 0;
        //    }

        //    return !list.Any();
        //}

        public static bool IsNullOrEmpty<T>(this T obj)
        {
            if (obj == null)
            {
                return true;
            }

            var asString = obj as string;
            if (asString != null)
            {
                return string.IsNullOrEmpty(asString) || string.IsNullOrWhiteSpace(asString);
            }

            var genericCollection = obj as ICollection<T>;
            if (genericCollection != null)
            {
                return genericCollection.Count == 0;
            }

            var nonGenericCollection = obj as ICollection;
            if (nonGenericCollection != null)
            {
                return nonGenericCollection.Count == 0;
            }

            return false;
        }

        public static string AsDescription(this System.Enum value)
        {
            try
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                var result = attributes.Length > 0 ? attributes[0].Description : value.ToString();
                return result;
            }
            catch
            {
                return value.ToString();
            }
        }
    }
}
