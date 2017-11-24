using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenious.Infrastructure.Helper;

namespace CRM.Areas.JJD.Controllers
{

    public class UserController : BaseController
    {
        public UserController() { }

        private readonly IG_UserService _IG_UserService;
        private readonly IG_UserDetailService _IG_UserDetailService;
        private readonly IG_OrderService _IG_OrderService;
        public UserController(IG_UserService iG_UserService,
                IG_UserDetailService _IG_UserDetailService,
                IG_OrderService _IG_OrderService)
        {
            this._IG_UserService = iG_UserService;
            this._IG_UserDetailService = _IG_UserDetailService;
            this._IG_OrderService = _IG_OrderService;
        }

        // GET: JJD/Login
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 账号密码登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            var model = this._IG_UserService.Login(new G_UserDTO { UserName = username, Password = password });

            if (model == null)
            {
                return Json(new MessageResult { Status = false, Message = "账号或密码错误" });
            }

            if (!model.IsActive)
            {
                return Json(new MessageResult { Status = false, Message = "账号已停用" });
            }

            //CacheService.Instance.Add(model.UserName, model.UserName, model, new DateTimeOffset().AddDays(1));

            Session["User"] = model;
            var url = "/jiajudai";
            switch (model.UserType)
            {
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.BC:
                    {
                        url = "/jiajudai/bankclerk";
                    }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.BM:
                    {
                        url = "/jiajudai/bankmanager";
                    }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.CL:
                    {
                        url = "/jiajudai/clientmanager";
                    }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.CS:
                    {
                        url = "/jiajudai/clientservice";
                    }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.SS:
                    { }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.ST:
                    { }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.US:
                    {
                        url = "/jiajudai/consumer";
                    }
                    break;
            }
            return Json(new MessageResult { Status = true, Message = "登录成功", Data = url });
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["User"] = null;
            System.Web.HttpContext.Current.Session.Clear();
            return RedirectToAction("index","home");
        }

        /// <summary>
        /// 手机验证码快速登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult QuickLogin(string username, string code)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(code))
            {
                return Json(new MessageResult { Status = false, Message = "手机号码或验证码为空" });
            }
            var user = this._IG_UserService.GetUserByUserName(username);
            if (user == null)
            {
                return Json(new MessageResult { Status = false, Message = "账号不存在" });
            }

            var oldCode = CacheService.Instance.Get(username, username);
            if (oldCode == null)
            {
                return Json(new MessageResult { Status = false, Message = "验证码失效" });
            }

            if (!oldCode.ToString().Equals(code))
            {
                return Json(new MessageResult { Status = false, Message = "验证码错误" });
            }

            Session["User"] = user;
            var url = "/jiajudai";
            switch (user.UserType)
            {
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.BC:
                    { }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.BM:
                    { }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.CL:
                    {
                        url = "/jiajudai/clientmanager";
                    }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.CS:
                    { }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.SS:
                    { }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.ST:
                    { }
                    break;
                case Ingenious.Infrastructure.Enum.G_UserTypeEnum.US:
                    {
                        url = "/jiajudai/consumer";
                    }
                    break;
            }
            return Json(new MessageResult { Status = true, Message = "登录成功", Data = url });
        }

        [AllowAnonymous]
        public ActionResult Register01()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register02(string phoneNo)
        {
            ViewBag.phoneNo = phoneNo;
            var oldCode = CacheService.Instance.Get(phoneNo, phoneNo);
            if (oldCode == null)
            {
                return RedirectToAction("register01");
            }

            return View();
        }

        /// <summary>
        /// 获取手机验证码(需要验证手机号码)
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns>返回验证码，如果发送失败则返回空</returns>
        [AllowAnonymous]
        public JsonResult GetSecurityCode(string phoneNo)
        {
            if (string.IsNullOrWhiteSpace(phoneNo) || !this.HasUserName(phoneNo))
            {
                return Json(new MessageResult { Status = false, Message = "不存在此账号" }, JsonRequestBehavior.AllowGet);
            }

            var code = Ingenious.Infrastructure.Message.MessageService.SMSSecurityCode(phoneNo);

            CacheService.Instance.Add(phoneNo, phoneNo, code, DateTimeOffset.Now.AddMinutes(2));
            return Json(new MessageResult { Status = true, Message = "验证码已发送" }, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 获取手机验证码
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns>返回验证码，如果发送失败则返回空</returns>
        [AllowAnonymous]
        public JsonResult GetSecurityCodeCommon(string phoneNo)
        {
            if (string.IsNullOrWhiteSpace(phoneNo))
            {
                return Json(new MessageResult { Status = false, Message = "手机号码为空" }, JsonRequestBehavior.AllowGet);
            }

            var code = Ingenious.Infrastructure.Message.MessageService.SMSSecurityCode(phoneNo);

            CacheService.Instance.Add(phoneNo, phoneNo, code, DateTimeOffset.Now.AddMinutes(2));
            return Json(new MessageResult { Status = true, Message = "验证码已发送" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取手机验证码(注册，需要判断是否存在手机号码)
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <returns>返回验证码，如果发送失败则返回空</returns>
        [AllowAnonymous]
        public JsonResult GetSecurityCodeForRegister(string phoneNo)
        {
            if (string.IsNullOrWhiteSpace(phoneNo))
            {
                return Json(new MessageResult { Status = false, Message = "手机号码为空" }, JsonRequestBehavior.AllowGet);
            }

            if (this.HasUserName(phoneNo))
            {
                return Json(new MessageResult { Status = false, Message = "已存在此手机号" }, JsonRequestBehavior.AllowGet);
            }

            var code = Ingenious.Infrastructure.Message.MessageService.SMSSecurityCode(phoneNo);

            CacheService.Instance.Add(phoneNo, phoneNo, code, DateTimeOffset.Now.AddMinutes(2));

            return Json(new MessageResult { Status = true, Message = "验证码已发送" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据验证码验证手机
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CheckSecurityCode(string phoneNo, string code)
        {
            var oldCode = CacheService.Instance.Get(phoneNo, phoneNo);
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
            CacheService.Instance.Add(key, "", phoneNo, DateTimeOffset.Now.AddMinutes(2));

            return Json(new MessageResult { Status = true, Message = phoneNo }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public bool HasUserName(string userName)
        {
            var user = this._IG_UserService.GetUserByUserName(userName);
            return user != null;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [System.Web.Http.HttpPost]
        public ActionResult Register(G_UserDTO user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName)
                || string.IsNullOrWhiteSpace(user.Password)
                || (user.Password.Length < 6 && user.Password.Length > 18))
            {
                return Json(new MessageResult { Status = false, Message = "资料输入有误" }, JsonRequestBehavior.AllowGet);
            }

            var hasUserName = this._IG_UserService.GetUserByUserName(user.UserName) != null;
            if (hasUserName)
            {
                return Json(new MessageResult { Status = hasUserName, Message = hasUserName ? "手机号码已存在" : "用户名不存在" }, JsonRequestBehavior.AllowGet);
            }
            string password = user.Password;
            user.Id = user.CreatedBy = user.ModifiedBy = Guid.NewGuid();
            user.Password = user.Password.ToMD5String();
            user.UserType = Ingenious.Infrastructure.Enum.G_UserTypeEnum.US;
            if (user.G_UserDetail == null)
                user.G_UserDetail = new G_UserDetailDTO();
            user.G_UserDetail.PersonalPhone = user.UserName;
            user.G_UserDetail.Name = "未设置";
            user.G_UserDetail.NickName = "未设置";

            user = this._IG_UserService.Create(user);
            this.Login(user.UserName, password);
            return Json(new MessageResult { Status = true, Message = "注册成功" }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword01()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword01(string phoneNo, string code)
        {
            var oldCode = CacheService.Instance.Get(phoneNo, phoneNo);
            if (oldCode == null)
            {
                ViewBag.error = "验证码失效";
                return View();
            }

            if (!oldCode.ToString().Equals(code))
            {
                ViewBag.error = "验证码错误";
                return View();
            }
            return RedirectToAction("forgotpassword02", new { username = phoneNo });
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword02(string username)
        {
            ViewBag.username = username;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ForgotPassword02(string username, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 6 || newPassword.Length > 18)
            {
                ViewBag.result = "新密码必须是6~18位";
                return View();
            }
            var user = this._IG_UserService.GetUserByUserName(username);
            if (user == null)
            {
                ViewBag.result = "账号不存在";
                return View();
            }

            user.Password = newPassword.ToMD5String();
            user.ModifiedBy = user.Id;
            this._IG_UserService.Update(new List<G_UserDTO> { user });
            ViewBag.result = "密码修改成功";
            return View();
        }


    }
}