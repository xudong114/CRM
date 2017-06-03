using CRM.Areas.ManagementCenter.Models;
using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenious.Infrastructure.Helper;

namespace CRM.Areas.ManagementCenter.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _IUserService;
        private readonly IUserDetailService _IUserDetailService;
        private readonly IDepartmentService _IDepartmentService;
        public UserController(IUserService iUserService,
            IDepartmentService iDepartmentService,
            IUserDetailService iUserDetailService)
        {
            this._IUserService = iUserService;
            this._IUserDetailService = iUserDetailService;
            this._IDepartmentService = iDepartmentService;
        }

        // GET: ManagementCenter/User
        public ActionResult Index(Guid? department = null, UserStatusEnum status = UserStatusEnum.All, string keywords = "")
        {
            var userDTO = _IUserService.GetUsers(department,status, keywords);
            ViewBag.keywords = keywords;
            ViewBag.department = department;
            this.DataBind();
            return View(userDTO);
        }

        public ActionResult Create()
        {
            var model = new UserDTOComposite();
            model.DepartmentDTOList = this._IDepartmentService.GetAll();

            return View(model);
        }
        //[Bind(Include = "Id,UserName,BranchId,DepartmentId,IsAdmin,IsSupper")]
        [HttpPost]
        public ActionResult Create(UserDTOComposite userDTOComposite)
        {
            var user = userDTOComposite.UserDTO;
            if (ModelState.IsValid)
            {
                var userDetail = new UserDetailDTO
                {
                    Name = user.UserDetail.Name,
                    CreatedBy = user.CreatedBy,
                    ModifiedBy = user.ModifiedBy,
                    CreatedDate = user.CreatedDate,
                    ModifiedDate = user.ModifiedDate
                };
                userDetail = this._IUserDetailService.Create(userDetail);
                user.UserDetailId = userDetail.Id;
                user.Status = UserStatusEnum.Available;
                user.Password =Ingenious.Infrastructure.GlobalMessage.DefaultPasswordFormat.ToMD5String();
                user.CreatedBy = user.ModifiedBy = this.User.Id;
                user = this._IUserService.Create(user);
                return RedirectToAction("Index");
            }
            this.DataBind();
            var model = new UserDTOComposite();
            model.DepartmentDTOList = this._IDepartmentService.GetAll();
            model.UserDTO = user;
            return View(model);
        }

        private void DataBind()
        {
            ViewBag.Departments = new SelectList(this._IDepartmentService.GetAll(), "Id", "Name");
        }
    }
}