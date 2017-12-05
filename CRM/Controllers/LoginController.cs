using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _UserService;
        public LoginController(IUserService userService)
        {
            this._UserService = userService;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                var model = this._UserService.Login(user);
                if (model == null)
                {
                    ViewBag.ERROR = "[无法登录]\\n\\t账号或密码错误";
                    return View("Index");
                }
                else
                {
                    if (model.Status != Ingenious.Infrastructure.Enum.UserStatusEnum.Available)
                    {
                        switch (model.Status)
                        {
                            case Ingenious.Infrastructure.Enum.UserStatusEnum.Departured:
                                {
                                    ViewBag.ERROR = "[无法登录]\\n\\t账号所属员工已离职";
                                }
                                break;
                            case Ingenious.Infrastructure.Enum.UserStatusEnum.Disabled:
                                {
                                    ViewBag.ERROR = "[无法登录]\\n\\t账号已禁用";
                                }
                                break;
                            case Ingenious.Infrastructure.Enum.UserStatusEnum.Locked:
                                {
                                    ViewBag.ERROR = "[无法登录]\\n\\t账号已锁定";
                                }
                                break;
                        }
                        return View("Index");
                    }

                    if (!model.Branch.IsActive)
                    {
                        ViewBag.ERROR = "[无法登录]\\n\\t账号部门已删除";
                        return View("Index");
                    }
                }

                System.Web.HttpContext.Current.Session["User"] = model;
                ViewBag.User = model;
                return RedirectToAction("Index", "Home");
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            System.Web.HttpContext.Current.Session["User"] = null;
            System.Web.HttpContext.Current.Session.Clear();
            return RedirectToAction("Index");
        }


    }
}