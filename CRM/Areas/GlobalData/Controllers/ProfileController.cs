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
        private readonly IBase_AccountService _IBase_AccountService;
        private readonly IBase_RealestateService _IBase_RealestateService;
        public ProfileController(IBase_ProfileService iBase_ProfileServicel,
            IBase_CarService iBase_CarService,
            IBase_FactoryService iBase_FactoryService,
            IBase_StoreService iBase_StoreService,
            IBase_AccountService iBase_AccountService,
            IBase_RealestateService iBase_RealestateService) 
        {
            this._IBase_ProfileServicel = iBase_ProfileServicel;
            this._IBase_CarService = iBase_CarService;
            this._IBase_FactoryService = iBase_FactoryService;
            this._IBase_StoreService = iBase_StoreService;
            this._IBase_AccountService = iBase_AccountService;
            this._IBase_RealestateService = iBase_RealestateService;
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
            model.AccountList = this._IBase_AccountService.GetAccountByIDNo(id);
            model.RealestateList = this._IBase_RealestateService.GetRealestateByIDNo(id);
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


        public ActionResult Edit(Guid id)
        {
            var model = new ProfileComplexModel();
            model.Profile = this._IBase_ProfileServicel.GetByKey(id);

            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this._IBase_ProfileServicel.Update(new List<Ingenious.DTO.Base_ProfileDTO> { model.Profile });
                }
                catch (Exception e)
                {
                    return Json(new MessageResult { Status = false, Message = e.InnerException.InnerException.Message });
                }
                return Json(new MessageResult { Status = true, Message = "编辑成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg() });
        }


        public ActionResult Car(string id)
        {
            ViewBag.idno = id;
            return View(new ProfileComplexModel() { Car = new Ingenious.DTO.Base_CarDTO { PurchasedDate = DateTime.Now } });
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

        public ActionResult CarEdit(Guid id)
        {
            var model = new ProfileComplexModel();
            model.Car = this._IBase_CarService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult CarEdit(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Car.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }
                var car = this._IBase_CarService.Update(new List<Ingenious.DTO.Base_CarDTO> { model.Car });

                return Json(new MessageResult { Status = true, Message = "编辑成功" });
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

        public ActionResult FactoryEdit(Guid id)
        {
            var model = new ProfileComplexModel();
            model.Factory = this._IBase_FactoryService.GetByKey(id);

            return View(model);
        }

        [HttpPost]
        public JsonResult FactoryEdit(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Factory.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }

                var car = this._IBase_FactoryService.Update(new List<Ingenious.DTO.Base_FactoryDTO> { model.Factory });

                return Json(new MessageResult { Status = true, Message = "编辑成功" });
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

        public ActionResult StoreEdit(Guid id)
        {
            var model = new ProfileComplexModel();
            model.Store = this._IBase_StoreService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult StoreEdit(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Store.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }

                var car = this._IBase_StoreService.Update(new List<Ingenious.DTO.Base_StoreDTO> { model.Store });

                return Json(new MessageResult { Status = true, Message = "编辑成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg(true) });
        }

        public ActionResult Account(string id)
        {
            ViewBag.idno = id;
            return View();
        }

        [HttpPost]
        public JsonResult Account(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Account.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }
                var car = this._IBase_AccountService.Create(model.Account);

                return Json(new MessageResult { Status = true, Message = "创建成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg() });
        }

        public ActionResult AccountEdit(Guid id)
        {
            var model = new ProfileComplexModel();
            model.Account = this._IBase_AccountService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult AccountEdit(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Account.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }
                var car = this._IBase_AccountService.Update(new List<Ingenious.DTO.Base_AccountDTO> { model.Account });

                return Json(new MessageResult { Status = true, Message = "编辑成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg() });
        }

        public ActionResult Realestate(string id)
        {
            ViewBag.idno = id;
            var model = new ProfileComplexModel() { Realestate = new Ingenious.DTO.Base_RealestateDTO() };
            return View(model);
        }

        [HttpPost]
        public JsonResult Realestate(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Realestate.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }
                var car = this._IBase_RealestateService.Create(model.Realestate);

                return Json(new MessageResult { Status = true, Message = "创建成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg() });
        }

        public ActionResult RealestateEdit(Guid id)
        {
            var model = new ProfileComplexModel();
            model.Realestate = this._IBase_RealestateService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult RealestateEdit(ProfileComplexModel model)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(model.Realestate.IDNo))
                {
                    return Json(new MessageResult { Status = false, Message = "身份证号码不能为空" });
                }
                var car = this._IBase_RealestateService.Update(new List<Ingenious.DTO.Base_RealestateDTO> { model.Realestate });

                return Json(new MessageResult { Status = true, Message = "编辑成功" });
            }

            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg() });
        }

    }
}