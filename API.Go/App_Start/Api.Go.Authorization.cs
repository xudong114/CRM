using Ingenious.DTO;
using Ingenious.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace API.Go.App_Start
{
    public class Api_Go_Authorization : AuthorizeAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var token = string.Empty;
            var userId = string.Empty;
            var authorization = actionContext.Request.Headers.Authorization;
            if (authorization != null && authorization.Parameter != null)
            {
                var user = APICacheService.Instance.Get(authorization.Parameter);
                if(user!=null)
                {
                    base.OnAuthorization(actionContext);
                }
                else
                {
                    this.HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                var allowAnonymous = false;
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();

                var enumerator = attributes.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current is AllowAnonymousAttribute)
                        allowAnonymous = true;
                }
                if (allowAnonymous)
                    base.OnAuthorization(actionContext);
                else
                    this.HandleUnauthorizedRequest(actionContext);
            }

        }

        private void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            System.Web.HttpContext.Current.Response.Write("{Status:false,Message:'对该请求的授权已被拒绝'}");
            System.Web.HttpContext.Current.Response.End();
        }

    }
}