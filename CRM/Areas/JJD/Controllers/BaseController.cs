using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJD.Controllers
{
    public class BaseController : Controller
    {
        public G_UserDTO User { get; set; }

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
            var allowAnonymous = false;

            var attributes = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false);

            attributes.ToList().AddRange(filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), false));

            var enumerator = attributes.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is AllowAnonymousAttribute)
                    allowAnonymous = true;
            }

            var user = System.Web.HttpContext.Current.Session["User"] as G_UserDTO;
            if (user == null && !allowAnonymous)
            {
                System.Web.HttpContext.Current.Response.Redirect("/jiajudai", true);
            }

            if(user!=null)
            {
                this.User = user;
                ViewBag.User = user;
                ViewBag.UserName = user.UserName;
            }

            base.OnAuthentication(filterContext);
        }
    }
}