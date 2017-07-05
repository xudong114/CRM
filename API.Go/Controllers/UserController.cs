using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure.Cache;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Go.Controllers
{

    public class UserController : BaseController
    {
        public UserController() { }

        private readonly IF_UserService _IF_UserService;
        public UserController(IF_UserService iF_UserService)
        {
            this._IF_UserService = iF_UserService;
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
                return Json<dynamic> (new { Status = false, Message = "账号或密码错误。" });
            }

            if (!user.IsActive)
            {
                return Json<dynamic>(new { Status = false, Message = "账号已停用。" });
            }

            return Json<dynamic>(new { Status = true, Message = "登录成功。" });
        }

        [AllowAnonymous]
        public IHttpActionResult Login(string username,string password)
        {
            return this.Get(username, password);
        }
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

        [AllowAnonymous]
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult CheckSecurityCode(string phoneNo,string code)
        {
            var oldCode = APICacheService.Instance.Get(phoneNo, phoneNo);
            if(oldCode==null )
            {
                return Json<dynamic>(new { Status = false, Message = "验证码失效" });
            }
            if( !oldCode.ToString().Equals(code))
            {
                return Json<dynamic>(new { Status = false, Message = "验证码错误" });
            }
            return Json<dynamic>(new { Status = true, Message = "验证成功" });
        }

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
        [System.Web.Http.AcceptVerbs("GET", "POST")]
        [System.Web.Http.HttpPost]
        public IHttpActionResult Register([FromBody] F_UserDTO user)
        {
            user.Id = user.CreatedBy = user.ModifiedBy = Guid.NewGuid();
            user = this._IF_UserService.Create(user);
            return Json<dynamic>(new { Status = user != null, Message = user == null ? "注册失败" : "注册成功" });
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        //[System.Web.Http.AcceptVerbs("GET", "POST")]
        //[System.Web.Http.HttpPost]
        //public IHttpActionResult Register(string userName,string password,F_UserType userType)
        //{
        //    var user = new F_UserDTO();
        //    user.UserName = userName;
        //    user.Password = password;
        //    user.UserType = userType;
        //    user.Id = user.CreatedBy = user.ModifiedBy = Guid.NewGuid();
        //    user = this._IF_UserService.Create(user);
        //    return Json<dynamic>(new { Status = user != null, Message = user == null ? "注册失败" : "注册成功" });
        //}


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