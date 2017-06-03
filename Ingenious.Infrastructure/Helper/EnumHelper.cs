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

    }
}
