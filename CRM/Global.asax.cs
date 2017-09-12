using CRM.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CRM
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UnityConfig.RegisterComponents();
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            var error = Server.GetLastError();
            
            if(error is HttpException)
            {
                var httpError = error as HttpException;
            }
            else
            {
                Response.StatusCode = 500;
            }
            
            Application["PATH"] = Request.Url.ToString();
            Application["MESSAGE"] = error.Message;
            Server.ClearError();
        }
    }
}
