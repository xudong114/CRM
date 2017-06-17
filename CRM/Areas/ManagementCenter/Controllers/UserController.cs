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
using Ingenious.Infrastructure;

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
                    Logo="",
                    Name = user.UserDetail.Name,
                    CreatedBy = this.User.Id,
                    ModifiedBy = this.User.Id,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsActive=true,
                    Version=1
                };
                //userDetail = this._IUserDetailService.Create(userDetail);
                user.UserDetail = userDetail;
                //user.UserDetailId = userDetail.Id;
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

        public ActionResult Edit(Guid id)
        {
            var model = new UserDTOComposite();
            model.DepartmentDTOList = this._IDepartmentService.GetAll();
            model.UserDTO = this._IUserService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(UserDTOComposite userDTOComposite)
        {
            var user = userDTOComposite.UserDTO;
            if (ModelState.IsValid)
            {
                this._IUserService.Update(new UserDTOList { user });

                return RedirectToAction("Index");
            }
            this.DataBind();
            var model = new UserDTOComposite();
            model.DepartmentDTOList = this._IDepartmentService.GetAll();
            model.UserDTO = user;
            return View(model);
        }

        public void Resume(Guid id)
        {
            List<UserDTO> dtoList = new List<UserDTO> { new UserDTO { Id = id, Status = UserStatusEnum.Available } };
            this._IUserService.Delete(dtoList);
        }
        public void Disable(Guid[] ids, bool status = false)
        {
            List<UserDTO> dtoList = new List<UserDTO>();
            ids.ToList().ForEach(item =>
            {
                dtoList.Add(new UserDTO { Id = item, Status = !status ? UserStatusEnum.Disabled : UserStatusEnum.Available,ModifiedBy=this.User.Id });
            });
            this._IUserService.Delete(dtoList);
        }

        private void DataBind()
        {
            ViewBag.Departments = new SelectList(this._IDepartmentService.GetAll(), "Id", "Name");
        }
    }
}