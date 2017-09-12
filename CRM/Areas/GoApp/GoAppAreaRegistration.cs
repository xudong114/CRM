using System.Web.Mvc;

namespace CRM.Areas.GoApp
{
    public class GoAppAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "GoApp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "GoApp_home",
                "app",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.GoApp.Controllers" }
            );
            context.MapRoute(
                "GoApp_home_route_shenqingxinyongka",
                "app/shenqingxinyongka",
                new { action = "Create", controller = "CreditCardApplication", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.GoApp.Controllers" }
            );
            context.MapRoute(
                "GoApp_home_route_xinyongka",
                "app/xinyongka",
                new { action = "Index", controller = "CreditCardApplication", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.GoApp.Controllers" }
            );
            context.MapRoute(
                "GoApp_home_route_xinyongka_details",
                "app/xinyongka/{id}",
                new { action = "Details", controller = "CreditCardApplication", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.GoApp.Controllers" }
            );

            context.MapRoute(
                "GoApp_home_route",
                "app/{controller}/{action}/{id}",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.GoApp.Controllers" }
            );
            context.MapRoute(
                "GoApp_default",
                "GoApp/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.GoApp.Controllers" }
            );
        }
    }
}