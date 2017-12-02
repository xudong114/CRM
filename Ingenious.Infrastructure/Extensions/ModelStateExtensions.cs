using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ingenious.Infrastructure.Extensions
{
    public static class ModelStateExtensions
    {
        public static string GetErrorMsg(this ModelStateDictionary ModelState,bool format = false)
        {
            if(ModelState==null)
            {
                return string.Empty;
            }
            var errString = new StringBuilder();
            foreach(var item in ModelState.Values)
            {
               foreach(var err in  item.Errors)
               {
                   if (format)
                   {
                       errString.Append(string.Format("错误：{0}<br/>", err.ErrorMessage));
                   }
                   else
                   {
                       errString.Append(string.Format("错误：{0}", err.ErrorMessage));
                   }
               }
            }
            return errString.ToString();
        }
    }
}
