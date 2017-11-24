using System.Web.Mvc;

namespace CRM.Areas.GlobalData
{
    public class GlobalDataAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "GlobalData";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "GlobalData_default",
                "GlobalData/{controller}/{action}/{id}",
                new { action = "Index",controller="Home", id = UrlParameter.Optional },
                new string[] { "CRM.Areas.GlobalData.Controllers" }
            );

        }
    }
}