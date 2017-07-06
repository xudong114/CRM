using Ingenious.DTO;
using Ingenious.Infrastructure.Cache;
using System.Web;
using System.Web.Http;

namespace API.Go.Controllers
{
    [API.Go.App_Start.Api_Go_Authorization]
    public class BaseController : ApiController
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        public F_UserDTO User
        {
            get
            {
                //经过授权验证后，此User对象已存在于缓存中
                var authorization = base.ActionContext.Request.Headers.Authorization;

                var user = APICacheService.Instance.Get(authorization.Parameter);

                return user as F_UserDTO;
            }
        }

        public BaseController()
        {

        }
    }
}