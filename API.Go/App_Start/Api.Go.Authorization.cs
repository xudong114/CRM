using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace API.Go.App_Start
{
    public class Api_Go_Authorization : ActionFilterAttribute
    {

        public override bool IsDefaultAttribute()
        {
            return base.IsDefaultAttribute();
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //var principal = actionContext.RequestContext.Principal;

            //var user = System.Web.HttpContext.Current.Session["User"];
            

            base.OnActionExecuting(actionContext);
        }

        public override System.Threading.Tasks.Task OnActionExecutingAsync(System.Web.Http.Controllers.HttpActionContext actionContext, System.Threading.CancellationToken cancellationToken)
        {
            return base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}