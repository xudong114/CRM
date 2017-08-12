using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure
{
    /// <summary>
    /// 信息列表
    /// </summary>
    public class GlobalMessage
    {
        /// <summary>
        /// 默认密码格式
        /// </summary>
        public const string DefaultPasswordFormat = "123456";
        /// <summary>
        /// 默认头像
        /// </summary>
        public const string DefaultFace = "/Content/images/default_face.png";
        /// <summary>
        /// 数据字典：客户级别标识
        /// </summary>
        public const string DataItem_ClientGradeName = "客户级别";
        /// <summary>
        /// 数据字典：客户行业标识
        /// </summary>
        public const string DataItem_ClientIndustryName = "行业";
        /// <summary>
        /// 数据字典：客户动态类型标识
        /// </summary>
        public const string DataItem_ClientActivityCategory = "客户活动类型";

        #region Session

        /// <summary>
        /// Api 手机验证码 Session 标识
        /// </summary>
        public const string API_Session_SecurityCode = "API_Session_SecurityCode";
        /// <summary>
        /// Api 用户登录 Session 标识
        /// </summary>
        public const string API_Session_User = "API_Session_User{0}";
        /// <summary>
        /// Api 用户登录 token 标识
        /// </summary>
        public const string API_Session_User_Token = "API_Session_User_Token{0}";
        /// <summary>
        /// 重置密码时保存手机号码用于验证
        /// </summary>
        public const string API_Session_ResetPassword = "API_Session_ResetPassword{0}";

        #endregion


        /// <summary>
        /// 缓存过期时间（秒）
        /// </summary>
        public static long CacheTimeout
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["timeout"].ToString());
            }
        }
        /// <summary>
        /// 初始密码
        /// </summary>
        public static string API_InitPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["initpassword"].ToString();
            }
        }
        /// <summary>
        /// 手机验证码
        /// </summary>
        public const string SMS_SecurityCode = "您的本次验证码是{0}，如非本人操作请忽略。";
        /// <summary>
        /// 未授权操作提示
        /// </summary>
        public const string Unauthorized = "对该请求的授权已被拒绝";
    }
}
