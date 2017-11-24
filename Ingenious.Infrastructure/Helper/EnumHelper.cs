using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Ingenious.Infrastructure.Helper
{
    public static class EnumHelper
    {
        public static string Discription(this System.Enum enumType)
        {
            var attrs = enumType.GetType()
                .GetField(enumType.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs != null
                && attrs.Length > 0)
            {
                return (attrs[0] as DescriptionAttribute).Description;
            }
            return string.Empty;
        }

        public static List<T> ToList<T>(this System.Enum enumType)
        {
            var list = new List<T>();

            var enumerator = System.Enum.GetValues(typeof(T)).GetEnumerator();
            while(enumerator.MoveNext())
            {
                list.Add((T)(enumerator.Current));
            }
            return list;
        }

    }
}
