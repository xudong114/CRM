using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure.Cache;
using Ingenious.Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web.Http;

namespace API.Go.App_Start
{
    /// <summary>
    /// 授权验证
    /// </summary>
    public class Api_Go_AuthorizationAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 授权验证
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var token = string.Empty;
            var userId = string.Empty;
            var authorization = actionContext.Request.Headers.Authorization;

            if (authorization == null || authorization.Parameter == null)
            {
                var allowAnonymous = false;
                var attributes = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>();

                var enumerator = attributes.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (enumerator.Current is AllowAnonymousAttribute)
                        allowAnonymous = true;
                }

                if (!allowAnonymous)
                {
                    this.HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                var user = APICacheService.Instance.Get(authorization.Parameter);

                if (user == null)
                {
                    var userService = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IF_UserService)) as IF_UserService;
                    var fUser = userService.GetByKey(Guid.Parse(authorization.Parameter));
                    if (fUser != null)
                    {
                        APICacheService.Instance.Add(fUser.Id.ToString(), "", fUser, DateTimeOffset.Now.AddYears(1));
                    }
                    else
                    {
                        this.HandleUnauthorizedRequest(actionContext, "当前会话已丢失,请重新登录");
                    }
                }
            }
        }

        private void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext, string otherInfo = "")
        {
            if (string.IsNullOrWhiteSpace(otherInfo))
            {
                System.Web.HttpContext.Current.Response.Write("{Status:false,Message:'对该请求的授权已被拒绝'}");
            }
            else
            {
                System.Web.HttpContext.Current.Response.Write("{Status:false,Message:'" + otherInfo + "'}");
            }

            System.Web.HttpContext.Current.Response.End();
        }

    }
}