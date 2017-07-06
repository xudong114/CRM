using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace API.Go.Controllers
{
    /// <summary>
    /// 消息服务
    /// </summary>
    [AllowAnonymous]
    public class MessageController : BaseController
    {
        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns>返回验证码</returns>
        public string GetSecurityCode(string phoneNo)
        {
            var code = Utility.GetRandomNo(6);
            var result = MessageService.SMSSend(phoneNo,code);
            if(result.Status)
            {
                HttpContext.Current.Session[GlobalMessage.API_Session_SecurityCode] = new SMSSession { Code = code, ExpireTime = DateTime.Now.AddMinutes(1), PhoneNo = phoneNo };
            }

            return code;
        }


        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns>返回验证码</returns>
        public string Get(string phoneNo)
        {
            return this.GetSecurityCode(phoneNo);
        }

    }
}