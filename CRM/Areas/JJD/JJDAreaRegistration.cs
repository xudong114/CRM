using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;

namespace CRM.Areas.JJD
{
    public class JJDAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "JJD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "jiajudai",
                "jiajudai",
                new { action = "Index", controller = "Home", id = UrlParameter.Optional },
               new string[] { "CRM.Areas.JJD.Controllers" }
                );
            context.MapRoute(
                "jiajudai_login",
                "jiajudai/login",
                new { action = "Login", controller = "User" },
                new GuidRouteConstraint(),
                new string[] { "CRM.Areas.JJD.Controllers" }
            );
            context.MapRoute(
                "jiajudai_news",
                "jiajudai/news/{id}",
                new { action = "Details", controller = "News"},
                new GuidRouteConstraint(),
                new string[] { "CRM.Areas.JJD.Controllers" }
            );
            context.MapRoute(
                "jiajudai_product",
                "jiajudai/product/{id}",
                new { action = "Details", controller = "Product" },
                new GuidRouteConstraint(),
                new string[] { "CRM.Areas.JJD.Controllers" }
            );
            context.MapRoute(
                "jiajudai_default",
                "jiajudai/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.JJD.Controllers" }
                );
            context.MapRoute(
                "JJD_default",
                "JJD/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
               new string[] { "CRM.Areas.JJD.Controllers" }
            );

        }
    }
}