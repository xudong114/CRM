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
            if(ModelState.IsValid)
            {
                var model = this._UserService.Login(user);
                if(model == null )
                {
                    ModelState.AddModelError("error", "登录失败！");
                    return View("Index");
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