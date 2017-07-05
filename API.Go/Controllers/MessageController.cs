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
    [AllowAnonymous]
    public class MessageController : BaseController
    {
        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns></returns>
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


        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}