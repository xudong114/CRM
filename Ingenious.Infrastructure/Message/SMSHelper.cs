using Ingenious.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Message
{
    /// <summary>
    /// 手机短信
    /// </summary>
    public class SmsHelper
    {
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="content">发送内容</param>
        /// <returns></returns>
        public static MessageResult Send(string mobile, string content)
        {
            const string sn = "SDK-WSS-010-06882";
            string password = "7-3ce6-[";
            const string smsSignature = "【GO佳居】";
            //password = (sn + password).ToString();

            password = SecurityHelper.Md5Encrypt(sn + password, Encoding.Default).ToUpper();

            string ext = string.Empty;
            string stime = string.Empty;
            string rrid = string.Empty;
            string msgfmt = string.Empty;
            string code;
            using (var httpClient = new HttpClient())
            {
                string url = string.Format("http://sdk.entinfo.cn:8061/webservice.asmx/mdsmssend?sn={0}&pwd={1}&mobile={2}&content={3}&ext={4}&stime={5}&rrid={6}&msgfmt={7}",
                    sn, password, mobile, content + smsSignature, ext, stime, rrid, msgfmt);
                httpClient.BaseAddress = new Uri(url);
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
                var response = httpClient.GetAsync(url).Result;
                code = response.Content.ReadAsStringAsync().Result;
            }

            var message = CheckResult(code);

            return message;
        }

        /// <summary>
        /// 检测返回码的状态
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private static MessageResult CheckResult(string code)
        {
            var message = new MessageResult { Status = true };
            if (code.StartsWith("-"))
            {
                switch (code)
                {
                    case "-2":
                        message.Status = false;
                        message.Message = "序列号未注册或加密不对";
                        break;
                    case "-4":
                        message.Status = false;
                        message.Message = "余额不足支持本次发送";
                        break;
                    case "-5":
                        message.Status = false;
                        message.Message = "数据格式错误";
                        break;
                    case "-6":
                        message.Status = false;
                        message.Message = "参数有误";
                        break;
                    case "-7":
                        message.Status = false;
                        message.Message = "权限受限";
                        break;
                    case "-8":
                        message.Status = false;
                        message.Message = "流量控制错误";
                        break;
                    case "-9":
                        message.Status = false;
                        message.Message = "扩展码权限错误";
                        break;
                    case "-10":
                        message.Status = false;
                        message.Message = "内容长度长";
                        break;
                    case "-11":
                        message.Status = false;
                        message.Message = "内部数据库错误";
                        break;
                    case "-12":
                        message.Status = false;
                        message.Message = "序列号状态错误";
                        break;
                    case "-14":
                        message.Status = false;
                        message.Message = "服务器写文件失败";
                        break;
                    case "-17":
                        message.Status = false;
                        message.Message = "没有权限";
                        break;
                    case "-19":
                        message.Status = false;
                        message.Message = "禁止同时使用多个接口地址";
                        break;
                    case "-20":
                        message.Status = false;
                        message.Message = "相同手机号，相同内容重复提交";
                        break;
                    case "-22":
                        message.Status = false;
                        message.Message = "Ip鉴权失败";
                        break;
                    case "-23":
                        message.Status = false;
                        message.Message = "缓存无此序列号信息";
                        break;
                    case "-601":
                        message.Status = false;
                        message.Message = "序列号为空，参数错误";
                        break;
                    case "-602":
                        message.Status = false;
                        message.Message = "序列号格式错误，参数错误";
                        break;
                    case "-603":
                        message.Status = false;
                        message.Message = "密码为空，参数错误";
                        break;
                    case "604":
                        message.Status = false;
                        message.Message = "手机号码为空，参数错误";
                        break;
                    case "-605":
                        message.Status = false;
                        message.Message = "内容为空，参数错误";
                        break;
                    case "-606":
                        message.Status = false;
                        message.Message = "ext长度大于9，参数错误";
                        break;
                    case "-607":
                        message.Status = false;
                        message.Message = "参数错误 扩展码非数字";
                        break;
                    case "-608":
                        message.Status = false;
                        message.Message = "参数错误 定时时间非日期格式";
                        break;
                    case "-609":
                        message.Status = false;
                        message.Message = "rrid长度大于18,参数错误";
                        break;
                    case "-610":
                        message.Status = false;
                        message.Message = "参数错误 rrid非数字";
                        break;
                    case "-611":
                        message.Status = false;
                        message.Message = "参数错误 内容编码不符合规范";
                        break;
                    case "-623":
                        message.Status = false;
                        message.Message = "手机个数与内容个数不匹配";
                        break;
                    case "-624":
                        message.Status = false;
                        message.Message = "扩展个数与手机个数数";
                        break;
                    default:
                        message.Status = false;
                        message.Message = "错误，请调试程序";
                        break;
                }
            }
            else
            {
                message.Message = "发送成功";
            }
            return message;
        }


       
    }

    /// <summary>
    /// 短消息发送结果
    /// </summary>
    public struct MessageResult
    {
        public bool Status { get; set; }
        public string Message { get; set; }
    }

    /// <summary>
    /// 手机获取验证码与过期时间
    /// </summary>
    [Serializable]
    public class SMSSession
    {
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PhoneNo { get; set; }

    }

}
