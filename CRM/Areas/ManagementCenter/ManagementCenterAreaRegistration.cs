using System.Web.Mvc;

namespace CRM.Areas.ManagementCenter
{
    public class ManagementCenterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ManagementCenter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ManagementCenter_home",
                "admin",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.ManagementCenter.Controllers" }

            );

            context.MapRoute(
                "ManagementCenter_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.ManagementCenter.Controllers" }
            );
        }

    }
}