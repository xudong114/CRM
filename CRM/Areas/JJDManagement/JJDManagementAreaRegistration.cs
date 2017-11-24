using System.Web.Mvc;

namespace CRM.Areas.JJDManagement
{
    public class JJDManagementAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "JJDManagement";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "JJDManagement_index",
                "jiajudai/sys",
                new { action = "Index", controller = "Default", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.JJDManagement.Controllers" }
            );
            context.MapRoute(
                "JJDManagement_jiajudai",
                "jiajudai/sys/{controller}/{action}/{id}",
                new { action = "Index", controller = "Default", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.JJDManagement.Controllers" }
            );
            context.MapRoute(
                "JJDManagement_default",
                "JJDManagement/{controller}/{action}/{id}",
                new { action = "Index",controller="Default", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.JJDManagement.Controllers" }
            );
        }
    }
}