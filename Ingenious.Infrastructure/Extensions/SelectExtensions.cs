using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ingenious.Infrastructure.Extensions
{
    public static class SelectExtensions
    {
        public static MvcHtmlString EnumDropDownList(this HtmlHelper htmlHelper, Type enumType, string value, string htmlAttributes = "", string defaultOption = "")
        {
            var selectStr = new StringBuilder();
            selectStr.Append(string.Format("<select {0}>", htmlAttributes));

            if (!string.IsNullOrWhiteSpace(defaultOption))
            {
                selectStr.Append("<option>");
                selectStr.Append(defaultOption);
                selectStr.Append("</option>");
            }

            if (enumType.IsEnum)
            {
                foreach(var val in System.Enum.GetValues(enumType))
                {
                    var attrs = val.GetType()
                        .GetField(val.ToString())
                        .GetCustomAttributes(typeof(DescriptionAttribute), false);
                    if(attrs!=null 
                        && attrs.Length>0)
                    {
                        var descAttr = attrs[0] as DescriptionAttribute;
                        selectStr.Append(string.Format("<option value={0} {1}>", Convert.ToInt32(val), (Convert.ToInt32(val).ToString() == value) ? "selected='selected'" : ""));
                        selectStr.Append(descAttr.Description);
                        selectStr.Append("</option>");
                    }
                }
            }
            else
            {
                selectStr.Append("<option>");
                selectStr.Append(string.Format("{0} 不是枚举类型。", enumType.Name));
                selectStr.Append("</option>");
            }
            selectStr.Append("</select>");
            return new MvcHtmlString(selectStr.ToString());
        }

    }
}
