using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Message
{
    /// <summary>
    /// 消息管理服务
    /// </summary>
    public static class MessageService
    {
        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static MessageResult SMSSend(string phoneNo,string text)
        {
            //1、安全检验。
            //2、相同号码发送限制。
            //3、短时间请求次数限制。

            return SmsHelper.Send(phoneNo, text);
        }

        /// <summary>
        /// 发送手机验证码（4位数字）
        /// </summary>
        /// <param name="phoneNo">电话号码</param>
        /// <returns></returns>
        public static string SMSSecurityCode(string phoneNo)
        {

            var code = Ingenious.Infrastructure.Utility.GetRandomNo(4);
            var context = string.Format(GlobalMessage.SMS_SecurityCode, code);
            SmsHelper.Send(phoneNo, context);

            return code;
        }
    }
}
