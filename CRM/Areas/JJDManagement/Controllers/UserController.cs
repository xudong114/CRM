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

namespace CRM.Areas.JJDManagement.Controllers
{
    public class UserController : BaseController
    {
        public UserController() { }

        private readonly IG_UserService _IG_UserService;
        private readonly IG_UserDetailService _IG_UserDetailService;
        private readonly IG_BankService _IG_BankService;
        private readonly IG_EntityService _IG_EntityService;
        public UserController(IG_UserService IG_UserService,
            IG_UserDetailService IG_UserDetailService,
            IG_BankService IG_BankService,
            IG_EntityService IG_EntityService)
        {
            this._IG_UserService = IG_UserService;
            this._IG_UserDetailService = IG_UserDetailService;
            this._IG_BankService = IG_BankService;
            this._IG_EntityService = IG_EntityService;
        }

        // GET: GoApp/User
        public ActionResult Index(bool? isActive = null, string keywords = "", G_UserTypeEnum? userType = null, string sort = "createddate_desc")
        {
            var list = this._IG_UserService.GetUsers(isActive, keywords, userType, sort);
            return View(list);
        }

        public ActionResult Create()
        {
            this.DataBind();
            return View();
        }

        [HttpPost]
        public ActionResult Create(G_UserDTO user)
        {
            user.Password = GlobalMessage.API_InitPassword.ToMD5String();
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                this.DataBind();
                ModelState.AddModelError("username", "账号未设置");
                return RedirectToAction("create");
            }
            var userDetails = user.G_UserDetail;
            if (user.UserType == G_UserTypeEnum.BC
                || user.UserType == G_UserTypeEnum.BM)
            {
                if (userDetails == null
                    || string.IsNullOrWhiteSpace(userDetails.BankCode))
                {
                    this.DataBind();
                    ModelState.AddModelError("bank", "所属银行未设置");
                    return RedirectToAction("create");
                }
            }
            else
            {
                if (user.G_UserDetail != null)
                {
                    user.G_UserDetail.BankCode = "";
                }
            }

            user = this._IG_UserService.Create(user);

            if (user.IsAdmin)
            {
                if (user.G_EntityId != null)
                {
                    var entity = this._IG_EntityService.GetByKey(user.G_EntityId.Value);
                    entity.UserId = user.Id;
                    this._IG_EntityService.Update(new List<G_EntityDTO> { entity });
                }
            }


            return RedirectToAction("index");
        }

        public ActionResult Edit(Guid id)
        {
            this.DataBind();
            var user = this._IG_UserService.GetByKey(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(G_UserDTO user)
        {
            var oldUser = this._IG_UserService.GetByKey(user.Id);

            if (user.G_EntityId.HasValue)
            {
                oldUser.G_Entity = this._IG_EntityService.GetByKey(user.G_EntityId.Value);
                oldUser.G_EntityId = user.G_EntityId.Value;
                this._IG_UserService.Update(new List<G_UserDTO> { oldUser });
            }

            var userDetail = this._IG_UserDetailService.GetUserDetailByUserId(user.Id);
            if (userDetail != null)
            {
                userDetail.BankCode = user.G_UserDetail.BankCode;
                this._IG_UserDetailService.Update(new List<G_UserDetailDTO> { userDetail });
            }
            
            return RedirectToAction("index");
        }

        public ActionResult Delete(string[] ids)
        {
            var list = new List<G_UserDTO>();
            foreach (var item in ids)
            {
                list.Add(new G_UserDTO { Id = Guid.Parse(item) });
            }
            this._IG_UserService.Delete(list);
            return RedirectToAction("index");
        }

        public ActionResult Renew(Guid id)
        {
            var user = this._IG_UserService.GetByKey(id);
            if (user != null)
            {
                user.IsActive = true;
            }
            
            this._IG_UserService.Update(new List<G_UserDTO> { user });
                
            return RedirectToAction("index");
        }

        public ActionResult Details(Guid id)
        {
            var userdetail = this._IG_UserDetailService.GetUserDetailByUserId(id);
            return View(userdetail);
        }

        private void DataBind()
        {
            var banks = this._IG_BankService.GetBanks();
            var list = new SelectList(banks, "Code", "Name");
            ViewBag.Banks = list;

            var entities = this._IG_EntityService.GetAll(1, int.MaxValue);
            ViewBag.entities = new SelectList(entities.Data, "Id", "EntityName");
        }
    }
}