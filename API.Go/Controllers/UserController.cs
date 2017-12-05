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
        private readonly IF_AccountService _IF_AccountService;
        private readonly IF_OrderService _IF_OrderService;
        public UserController(IF_UserService iF_UserService,
            IF_UserDetailService iF_UserDetailService,
            IF_AccountService iF_AccountService,
            IF_OrderService iF_OrderService)
        {
            this._IF_UserService = iF_UserService;
            this._IF_UserDetailService = iF_UserDetailService;
            this._IF_AccountService = iF_AccountService;
            this._IF_OrderService = iF_OrderService;
        }


        [AllowAnonymous]
        public IHttpActionResult Get(string userName, string password)
        {

            var user = this._IF_UserService.Login(new F_UserDTO { UserName = userName, Password = password });

            if (user == null)
            {
                return Json(new MessageResult { Status = false, Message = "账号或密码错误。" });
            }

            if (!user.IsActive)
            {
                return Json(new MessageResult { Status = false, Message = "账号已停用" });
            }

            var token = Guid.NewGuid().ToString().ToLower();

            APICacheService.Instance.Add(token, "", user, DateTimeOffset.Now.AddDays(7));

            return Json<dynamic>(new { Status = true, Message = "登录成功", User = user, Token = token });
        }


        [AllowAnonymous]
        public IHttpActionResult Get(string userName, string password)
        {

            var user = this._IF_UserService.Login(new F_UserDTO { UserName = userName, Password = password });

            if (user == null)
            {
                return Json(new MessageResult { Status = false, Message = "账号或密码错误。" });
            }

            if (!user.IsActive)
            {
                return Json(new MessageResult { Status = false, Message = "账号已停用" });
            }

            var token = Guid.NewGuid().ToString().ToLower();

            APICacheService.Instance.Add(token, "", user, DateTimeOffset.Now.AddDays(7));

            return Json<dynamic>(new { Status = true, Message = "登录成功", User = user, Token = token });
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
            var user = this._IF_UserService.Login(new F_UserDTO { UserName = username, Password = password });

            if (user == null)
            {
                return Json(new MessageResult { Status = false, Message = "账号或密码错误。" });
            }

            if (!user.IsActive)
            {
                return Json(new MessageResult { Status = false, Message = "账号已停用" });
            }

            var token = user.Id.ToString().ToLower();
            
            APICacheService.Instance.Add(token, "", user, DateTimeOffset.Now.AddYears(1));
            return Json<dynamic>(new { Status = true, Message = "登录成功", User = user, Token = user.Id });

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
            var user = this._IF_UserService.Login(new F_UserDTO { UserName = username, Password = password });

            if (user == null)
            {
                return Json(new MessageResult { Status = false, Message = "账号或密码错误。" });
            }

            if (!user.IsActive)
            {
                return Json(new MessageResult { Status = false, Message = "账号已停用" });
            }

            var token = user.Id.ToString().ToLower();

            APICacheService.Instance.Add(token, "", user, DateTimeOffset.Now.AddYears(1));
            return Json<dynamic>(new { Status = true, Message = "登录成功", User = user, Token = user.Id });

        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        public IHttpActionResult Logout(string token)
        {
            APICacheService.Instance.Remove(token);

            return Json(new MessageResult { Status = true, Message = "退出成功" });
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

            APICacheService.Instance.Add(phoneNo, phoneNo, code, DateTimeOffset.Now.AddMinutes(2));

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
            APICacheService.Instance.Add(key, "", phoneNo, DateTimeOffset.Now.AddMinutes(2));
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
        public IHttpActionResult HasUserName(string userName)
        {
            var result = this._IF_UserService.GetUserByUserName(userName) != null;

            return Json(new MessageResult { Status = result, Message = result ? "用户名已存在" : "用户名不存在" });
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
            if (string.IsNullOrWhiteSpace(user.UserName)
                || string.IsNullOrWhiteSpace(user.Password)
                || (user.Password.Length < 6 && user.Password.Length > 18))
            {
                return Json(new MessageResult { Status = false, Message = "资料输入有误" });
            }

            var hasUserName = this._IF_UserService.GetUserByUserName(user.UserName) != null;
            if (hasUserName)
            {
                return Json(new MessageResult { Status = hasUserName, Message = hasUserName ? "用户名已存在" : "用户名不存在" });
            }

            user.Id = user.CreatedBy = user.ModifiedBy = Guid.NewGuid();
            user.Password = user.Password.ToMD5String();

            if (user.F_UserDetail == null)
                user.F_UserDetail = new F_UserDetailDTO();
            user.F_UserDetail.PersonalPhone = user.UserName;
            user.F_UserDetail.Name = "未设置";
            user.F_UserDetail.NickName = "未设置";

            user = this._IF_UserService.Create(user);
            return Json<dynamic>(new { Status = user != null, Message = user == null ? "注册失败" : "注册成功" });
        }

        /// <summary>
        /// 银行模块：注册客户经理
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult RegisterBankClerk(F_UserDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName)
                || user.F_UserDetail == null
                || string.IsNullOrWhiteSpace(user.F_UserDetail.Name)
                || string.IsNullOrWhiteSpace(user.F_UserDetail.PersonalPhone)
                //|| string.IsNullOrWhiteSpace(user.F_UserDetail.OfficePhone)
                || string.IsNullOrWhiteSpace(user.Password)
                || (user.Password.Length < 6 && user.Password.Length > 18))
            {
                return Json(new MessageResult { Status = false, Message = "资料输入有误" });
            }

            var hasUserName = this._IF_UserService.GetUserByUserName(user.UserName) != null;
            if (hasUserName)
            {
                return Json(new MessageResult { Status = hasUserName, Message = hasUserName ? "用户名已存在" : "用户名不存在" });
            }

            var createdUser = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);
            if (string.IsNullOrWhiteSpace(createdUser.BankCode))
            {
                return Json(new MessageResult { Status = false, Message = "无法创建账号，所属银行未设置" });
            }

            user.UserType = F_UserTypeEnum.BC;

            user.Id = user.CreatedBy = user.ModifiedBy = this.User.Id;
            user.Password = user.Password.ToMD5String();

            user.F_UserDetail.NickName = user.UserName;
            user.F_UserDetail.BankCode = createdUser.BankCode;
            user.F_UserDetail.Name = user.F_UserDetail.Name;
            user.F_UserDetail.PersonalPhone = user.F_UserDetail.PersonalPhone;
            user.F_UserDetail.OfficePhone = user.F_UserDetail.OfficePhone;

            user = this._IF_UserService.Create(user);
            return Json(new MessageResult { Status = user != null, Message = user == null ? "注册失败" : "注册成功" });
        }

        /// <summary>
        /// 修改导购信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateClerk(F_UserDetailDTO clerk)
        {
            if (clerk == null || string.IsNullOrWhiteSpace(clerk.Name))
            {
                return Json(new MessageResult { Status = false, Message = "姓名不能为空" });
            }
            var user = this._IF_UserDetailService.GetByKey(clerk.Id);
            if (user == null)
            {
                return Json(new MessageResult { Status = false, Message = "修改失败" });
            }

            user.Name = clerk.Name;
            user.ModifiedBy = this.User.Id;

            var list = this._IF_UserDetailService.Update(new List<F_UserDetailDTO> { user });

            return Json(new MessageResult { Status = true, Message = "修改成功" });
        }

        /// <summary>
        /// 重置密码
        /// 无法登录，使用手机验证码修改密码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <param name="password">新密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
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
        /// 修改密码
        /// 登录后输入原始密码来修改密码
        /// </summary>
        /// <param name="oldPassword">原始密码</param>
        /// <param name="newPassword">新密码</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult UpdatePassword(string oldPassword, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6)
            {
                return Json(new MessageResult { Status = false, Message = "新密码不能少于6位" });
            }
            var user = this._IF_UserService.GetByKey(this.User.Id);
            if (user == null)
            {
                return Json(new MessageResult { Status = false, Message = "账号不存在" });
            }

            if (!user.Password.Equals(oldPassword.ToMD5String()))
            {
                return Json(new MessageResult { Status = false, Message = "原始密码错误" });
            }
            user.Password = newPassword.ToMD5String();
            user.ModifiedBy = user.Id;
            this._IF_UserService.Update(new List<F_UserDTO> { user });

            return Json(new MessageResult { Status = true, Message = "密码重置成功" });
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult ResetPassword(Guid userId)
        {
            var user = this._IF_UserService.GetByKey(userId);
            user.Password = GlobalMessage.API_InitPassword.ToMD5String();
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
            var user = this._IF_UserDetailService.GetUserDetailByUserId(userDetail.F_UserId);
            var isExists = user != null;
            if (isExists)
            {
                if (string.IsNullOrWhiteSpace(userDetail.Name))
                {
                    return Json(new MessageResult { Status = false, Message = "真实姓名必填" });
                }
                userDetail.Id = user.Id;
                this._IF_UserDetailService.Update(new List<F_UserDetailDTO> { userDetail });
            }
            else
            {
                this._IF_UserDetailService.Create(userDetail);
            }
            return Json(new MessageResult { Status = true, Message = "保存成功" });
        }

        /// <summary>
        /// 编辑金融客服个人信息
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public IHttpActionResult EditClientManager(F_UserDetailDTO userDetail)
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(userDetail.F_UserId);
            var isExists = user != null;
            if (isExists)
            {
                if (string.IsNullOrWhiteSpace(userDetail.Name))
                {
                    return Json(new MessageResult { Status = false, Message = "真实姓名必填" });
                }

                user.Name = userDetail.Name;
                user.NickName = userDetail.NickName;
                user.Gender = userDetail.Gender;
                user.Province = userDetail.Province;
                user.City = userDetail.City;
                user.Country = userDetail.Country;
                user.Street = userDetail.Street;
                user.Email = userDetail.Email;
                user.Face = userDetail.Face;
                
                this._IF_UserDetailService.Update(new List<F_UserDetailDTO> { user });
            }
            else
            {
                this._IF_UserDetailService.Create(userDetail);
            }
            return Json(new MessageResult { Status = true, Message = "保存成功" });
        }

        /// <summary>
        /// 更新银行信贷经理信息
        /// </summary>
        /// <param name="userDetail">信贷经理信息</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateBankManager(F_UserDetailDTO userDetail)
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);
            user.Email = userDetail.Email;
            user.OfficePhone = userDetail.OfficePhone;
            user.Face = userDetail.Face;
            this._IF_UserDetailService.Update(new List<F_UserDetailDTO> { user });

            return Json(new MessageResult { Status = true, Message = "保存成功" });
        }

        /// <summary>
        /// 更新银行客户经理信息
        /// </summary>
        /// <param name="userDetail">客户经理信息</param>
        /// <returns></returns>
         [HttpPost]
        public IHttpActionResult UpdateBankClientManager(F_UserDetailDTO userDetail)
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);
            user.Email = userDetail.Email;
            user.OfficePhone = userDetail.OfficePhone;
            user.Face = userDetail.Face;
            this._IF_UserDetailService.Update(new List<F_UserDetailDTO> { user });

            return Json(new MessageResult { Status = true, Message = "保存成功" });
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <param name="userId">用户标志(F_User.Id)</param>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetUserDetail(Guid userId)
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(userId);
            return Json(user);
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetUserDetail()
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);
            return Json(new MessageResult { Status = true, Data = user });
        }

        /// <summary>
        /// 获取个人账户信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAccount()
        {
            var account = this._IF_AccountService.GetAccount(this.User.Id);
            var userDetail = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);
            if (userDetail != null)
            {
                account.LastGain = this._IF_OrderService.GetLastSuccessOrder(userDetail.Code).LoanAmount * 10000 * 0.01m;
            }

            return Json(new MessageResult { Status = true, Data = account });
        }

        /// <summary>
        /// 设置支付宝账户信息
        /// </summary>
        /// <param name="alipay">支付宝账号</param>
        /// <param name="name">真实姓名</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult SetAlipay(string alipay, string name)
        {
            if (string.IsNullOrWhiteSpace(alipay))
            {
                return Json(new MessageResult { Status = false, Message = "支付宝账号错误" });
            }
            var account = this._IF_AccountService.GetAccount(this.User.Id);
            if (account == null)
            {
                account = new F_AccountDTO();
                account.CreatedBy = account.ModifiedBy = this.User.Id;
                account.UserId = this.User.Id;
                account.Alipay = alipay;
                account.Name = name;
            }
            else
            {
                account.ModifiedBy = this.User.Id;
                account.Alipay = alipay;
                account.Name = name;
            }
            this._IF_AccountService.SetAlipay(account);
            return Json(new MessageResult { Status = true, Message = "支付宝账号绑定成功", Data = account });
        }

        /// <summary>
        /// 冻结用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult FrozenUser(Guid userId)
        {
            var list = new List<F_UserDTO> { new F_UserDTO { Id = userId, ModifiedBy = this.User.Id } };

            this._IF_UserService.Delete(list);

            return Json(new MessageResult { Status = true, Message = "冻结成功" });
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
            string aa = "123";
        }
    }

}