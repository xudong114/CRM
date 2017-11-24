using CRM.Areas.GlobalData.Models;
using Ingenious.Application.Interface;
using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenious.Infrastructure.Extensions;

namespace CRM.Areas.GlobalData.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController() { }
        private readonly IBase_ProfileService _IBase_ProfileServicel;
        private readonly IBase_CarService _IBase_CarService;
        private readonly IBase_FactoryService _IBase_FactoryService;
        private readonly IBase_StoreService _IBase_StoreService;
        public ProfileController(IBase_ProfileService iBase_ProfileServicel,
            IBase_CarService iBase_CarService,
            IBase_FactoryService iBase_FactoryService,
            IBase_StoreService iBase_StoreService) 
        {
            this._IBase_ProfileServicel = iBase_ProfileServicel;
            this._IBase_CarService = iBase_CarService;
            this._IBase_FactoryService = iBase_FactoryService;
            this._IBase_StoreService = iBase_StoreService;
        }

        // GET: GlobalData/Profile
        public ActionResult Index(int pageIndex = 1, string keyword = "")
        {
            const int pageSize = 20;
            var list = this._IBase_ProfileServicel.GetAll(pageIndex, pageSize, keyword);
            return View(list);
        }

        public ActionResult Details(string id)
        {
            var model = new ProfileComplexModel();
            model.Profile = this._IBase_ProfileServicel.GetProfileByIDNo(id);
            model.CarList = this._IBase_CarService.GetCarByIDNo(id);
            model.FactoryList = this._IBase_FactoryService.GetFactoryByIDNo(id);
            model.StoreList = this._IBase_StoreService.GetStoreByCode(id) ;
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this._IBase_ProfileServicel.Create(model.Profile);
                }
                catch (Exception e)
                {
                    return Json(new MessageResult { Status = false, Message = e.InnerException.InnerException.Message });
                }
                return Json(new MessageResult { Status = true, Message = "创建成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg() });
        }

        public ActionResult Car(string id)
        {
            ViewBag.idno = id;
            return View();
        }

        [HttpPost]
        public JsonResult Car(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if(string.IsNullOrWhiteSpace( model.Car.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }
                var car = this._IBase_CarService.Create(model.Car);
                
                return Json(new MessageResult { Status = true, Message = "创建成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg() });
        }


        public ActionResult Factory(string id)
        {
            ViewBag.idno = id;
            return View(new ProfileComplexModel() { Factory = new Ingenious.DTO.Base_FactoryDTO() });
        }

        [HttpPost]
        public JsonResult Factory(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Factory.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }

                var car = this._IBase_FactoryService.Create(model.Factory);

                return Json(new MessageResult { Status = true, Message = "创建成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg(true) });
        }

        public ActionResult Store(string id)
        {
            ViewBag.idno = id;
            return View(new ProfileComplexModel() {  });
        }

        [HttpPost]
        public JsonResult Store(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Store.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }

                var car = this._IBase_StoreService.Create(model.Store);

                return Json(new MessageResult { Status = true, Message = "创建成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg(true) });
        }
    }
}