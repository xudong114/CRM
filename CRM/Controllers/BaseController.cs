using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        public UserDTO User { get; set; }

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {

            return base.BeginExecute(requestContext, callback, state);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            base.OnActionExecuting(filterContext);
        }

        protected override void OnAuthentication(System.Web.Mvc.Filters.AuthenticationContext filterContext)
        {
            var user = System.Web.HttpContext.Current.Session["User"] as UserDTO;
            if (user == null)
            {
                System.Web.HttpContext.Current.Response.Redirect("/login", true);
            }

            if(user!=null)
            {
                this.User = user;
                ViewBag.User = user;
                ViewBag.UserName = user.UserName;
                if (user.UserName == "globaldata")
                {
                    System.Web.HttpContext.Current.Response.Redirect("/globaldata", true);
                }
            }

            base.OnAuthentication(filterContext);
        }
    }
}