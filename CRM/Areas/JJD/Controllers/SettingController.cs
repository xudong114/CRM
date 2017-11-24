using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJD.Controllers
{
    public class SettingController : BaseController
    {
        public SettingController() { }

        private readonly IG_UserService _IG_UserService;
        private readonly IG_UserDetailService _IG_UserDetailService;
        public SettingController(IG_UserService iG_UserService,
            IG_UserDetailService iG_UserDetailService)
        {
            this._IG_UserService = iG_UserService;
            this._IG_UserDetailService = iG_UserDetailService;
        }

        // GET: JJD/Setting
        public ActionResult Index(Guid id)
        {
            ViewBag.UserId = id;

            return View();
        }

        new public ActionResult Profile(Guid id)
        {
            var user = this._IG_UserDetailService.GetUserDetailByUserId(id);
            return View(user);
        }

        [HttpPost]
        new public ActionResult Profile(G_UserDetailDTO user)
        {
            if(ModelState.IsValid)
            {
                this._IG_UserDetailService.Update(new List<G_UserDetailDTO> { user });
            }

            return RedirectToAction("index", "setting", new { id = user.G_UserId });
        }
    }
}