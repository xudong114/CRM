using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Cache;
using Ingenious.Infrastructure.Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Web.Http;
using Ingenious.Infrastructure.Helper;
namespace API.Go.Controllers
{
    /// <summary>
    /// 账号服务
    /// </summary>
    public class UserController : BaseController
    {
        public UserController() { }

        private readonly IF_UserService _IF_UserService;
        private readonly IF_UserDetailService _IF_UserDetailService;

        public UserController(IF_UserService iF_UserService,
            IF_UserDetailService iF_UserDetailService)
        {
            this._IF_UserService = iF_UserService;
            this._IF_UserDetailService = iF_UserDetailService;
        }

        [AllowAnonymous]
        public IHttpActionResult Get(string userName, string password)
        {
            F_UserDTO user = this._IF_UserService.Login(new F_UserDTO { UserName = userName, Password = password });

            Newtonsoft.Json.JsonSerializerSettings jss = new Newtonsoft.Json.JsonSerializerSettings();
            jss.DateFormatString = "yyyy.MM.dd HH:mm:ss";
            jss.Formatting = Newtonsoft.Json.Formatting.Indented;
            jss.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            jss.MaxDepth = 2;
            jss.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

            if (user == null)
            {
                return Json(new MessageResult { Status = false, Message = "账号或密码错误。" });
            }

            if (!user.IsActive)
            {
                return Json(new MessageResult { Status = false, Message = "账号已停用。" });
            }

            var token = Guid.NewGuid().ToString().ToLower();
            var value = string.Format("{0} {1}", token, user.Id);

            APICacheService.Instance.Add(token, "", user, DateTimeOffset.Now.AddDays(7));

            return Json<dynamic>(new { Status = true, Message = "登录成功。", User = user, Token = token });
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Login(string username, string password)
        {
            return this.Get(username, password);
        }
        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns>返回验证码，如果发送失败则返回空</returns>
        [AllowAnonymous]
        public IHttpActionResult GetSecurityCode(string phoneNo)
        {
            var oldCode = APICacheService.Instance.Get(phoneNo, phoneNo);
            if (oldCode != null)
                return Json("");

            var code = Ingenious.Infrastructure.Message.MessageService.SMSSecurityCode(phoneNo);

            APICacheService.Instance.Add(phoneNo, phoneNo, code, DateTimeOffset.Now.AddMinutes(1));

            return Json(code);
        }

        /// <summary>
        /// 根据验证码验证手机
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult CheckSecurityCode(string phoneNo, string code)
        {
            var oldCode = APICacheService.Instance.Get(phoneNo, phoneNo);
            if (oldCode == null)
            {
                return Json(new MessageResult { Status = false, Message = "验证码失效" });
            }
            if (!oldCode.ToString().Equals(code))
            {
                return Json(new MessageResult { Status = false, Message = "验证码错误" });
            }
            //重置密码时候验证手机号码，10分钟后自动过期。
            var key = string.Format(GlobalMessage.API_Session_ResetPassword, phoneNo);
            APICacheService.Instance.Add(key, "", phoneNo, DateTimeOffset.Now.AddMinutes(10));
            return Json(new MessageResult { Status = true, Message = "验证成功" });
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult IsExistsUser(string userName)
        {
            var result = this._IF_UserService.GetUserByUserName(userName) != null;

            return Json<bool>(result);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Register(F_UserDTO user)
        {
            user.Id = user.CreatedBy = user.ModifiedBy = Guid.NewGuid();
            user.Password = user.Password.ToMD5String();
            user = this._IF_UserService.Create(user);
            return Json<dynamic>(new { Status = user != null, Message = user == null ? "注册失败" : "注册成功" });
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <param name="password">新密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public IHttpActionResult ResetPassword(string phoneNo, string password)
        {
            var code = APICacheService.Instance.Get(string.Format(GlobalMessage.API_Session_ResetPassword, phoneNo), "");
            var user = this._IF_UserService.GetUserByUserName(phoneNo);
            if (user == null)
            {
                return Json(new MessageResult { Status = false, Message = "账号不存在" });
            }
            if (code == null || code.ToString() != phoneNo)
            {
                return Json(new MessageResult { Status = false, Message = "手机号码错误，请重新验证" });
            }
            user.Password = password.ToMD5String();
            user.ModifiedBy = user.Id;
            this._IF_UserService.Update(new List<F_UserDTO> { user });
            return Json(new MessageResult { Status = true, Message = "密码重置成功" });
        }

        /// <summary>
        /// 编辑个人信息
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult EditUserDetail(F_UserDetailDTO userDetail)
        {
            var isExists = this._IF_UserDetailService.GetUserDetailByUserId(userDetail.F_UserId) != null;
            if (isExists)
            {
                this._IF_UserDetailService.Update(new List<F_UserDetailDTO> { userDetail });
            }
            else
            {
                this._IF_UserDetailService.Create(userDetail);
            }
            return Json(new MessageResult { Status = true, Message = "更新成功" });
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