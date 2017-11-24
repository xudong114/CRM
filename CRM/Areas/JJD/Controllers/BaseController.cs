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

            //var attributes = new List<dynamic>();

            object[] controllerAttrs = filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);

            if (controllerAttrs != null)
            {
                controllerAttrs.ToList().ForEach(item =>
                {
                    if (item is AllowAnonymousAttribute)
                        allowAnonymous = true;
                });
            }

            object[] actionAttrs = filterContext.ActionDescriptor.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);
            if (actionAttrs != null)
            {
                actionAttrs.ToList().ForEach(item =>
                {
                    if (item is AllowAnonymousAttribute)
                        allowAnonymous = true;
                });
            }

            //var enumerator = attributes.GetEnumerator();

            //while (enumerator.MoveNext())
            //{
            //    if (enumerator.Current is AllowAnonymousAttribute)
            //        allowAnonymous = true;
            //}

            var user = System.Web.HttpContext.Current.Session["User"] as G_UserDTO;
            if (user == null && !allowAnonymous)
            {
                System.Web.HttpContext.Current.Response.Redirect("/jiajudai/login", true);
            }
            //user = new G_UserDTO();
            if (user != null)
            {
                this.User = user;
                ViewBag.User = user;
                ViewBag.UserName = user.UserName;
            }

            base.OnAuthentication(filterContext);
        }
    }
}