using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenious.Infrastructure.Helper;

namespace CRM.Areas.GoApp.Controllers
{
    public class UserController : BaseController
    {
        public UserController() { }

        private readonly IF_UserService _IF_UserService;
        private readonly IF_UserDetailService _IF_UserDetailService;
        private readonly IF_BankService _IF_BankService;
        public UserController(IF_UserService iF_UserService,
            IF_UserDetailService iF_UserDetailService,
            IF_BankService iF_BankService)
        {
            this._IF_UserService = iF_UserService;
            this._IF_UserDetailService = iF_UserDetailService;
            this._IF_BankService = iF_BankService;
        }

        // GET: GoApp/User
        public ActionResult Index(bool? isActive = null, string keywords = "", F_UserTypeEnum? userType = null, string sort = "createddate_desc")
        {
            var list = this._IF_UserService.GetUsers(isActive, keywords, userType, sort);
            return View(list);
        }

        public ActionResult Create()
        {
            this.DataBind();
            return View();
        }

        [HttpPost]
        public ActionResult Create(F_UserDTO user)
        {
            user.Password = GlobalMessage.API_InitPassword.ToMD5String();
            if(string.IsNullOrWhiteSpace(user.UserName))
            {
                this.DataBind();
                ModelState.AddModelError("username", "账号未设置");
                return RedirectToAction("create");
            }
            var userDetails = user.F_UserDetail;
            if(user.UserType== F_UserTypeEnum.BC
                || user.UserType== F_UserTypeEnum.BM)
            {
                if(userDetails==null
                    || string.IsNullOrWhiteSpace(userDetails.BankCode))
                {
                    this.DataBind();
                    ModelState.AddModelError("bank", "所属银行未设置");
                    return RedirectToAction("create");
                }
            }
            else
            {
                if (user.F_UserDetail != null)
                {
                    user.F_UserDetail.BankCode = "";
                }
            }
            
            this._IF_UserService.Create(user);
            return RedirectToAction("index");
        }

        public ActionResult Edit(Guid id)
        {
            var user = this._IF_UserService.GetByKey(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(F_UserDTO user)
        {
            this._IF_UserService.Update(new List<F_UserDTO> { user });
            return RedirectToAction("index");
        }

        public ActionResult Delete(string[] ids)
        {
            var list = new List<F_UserDTO>();
            foreach (var item in ids)
            {
                list.Add(new F_UserDTO { Id = Guid.Parse(item) });
            }
            this._IF_UserService.Delete(list);
            return RedirectToAction("index");
        }

        public ActionResult Renew(Guid id)
        {
            var user = this._IF_UserService.GetByKey(id);
            if(user!=null)
            {
                user.IsActive = true;
            }
            this._IF_UserService.Update(new List<F_UserDTO> { user });
            return RedirectToAction("index");
        }

        public ActionResult Details(Guid id)
        {
            var userdetail = this._IF_UserDetailService.GetUserDetailByUserId(id);
            return View(userdetail);
        }

        private void DataBind()
        {
            var banks = this._IF_BankService.GetBanks("", true);
            var list = new SelectList(banks, "Code", "Name");
            ViewBag.Banks = list;
        }
    }
}