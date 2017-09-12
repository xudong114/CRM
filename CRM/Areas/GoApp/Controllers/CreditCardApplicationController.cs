using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GoApp.Controllers
{
    public class CreditCardApplicationController : Controller
    {
        private readonly IF_CreditCardApplicationService _IF_CreditCardApplicationService;
        public CreditCardApplicationController(IF_CreditCardApplicationService iF_CreditCardApplicationService)
        {
            this._IF_CreditCardApplicationService = iF_CreditCardApplicationService;
        }
        // GET: GoApp/CreditCardApplication
        public ActionResult Index()
        {
            var list = this._IF_CreditCardApplicationService.GetAll();
            return View(list);
        }

        [AllowAnonymous]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Create(F_CreditCardApplicationDTO model)
        {
            if (ModelState.IsValid)
            {
                this._IF_CreditCardApplicationService.Create(model);
                return Json(new { Status = true });
            }
            var message = "";
            var errorList = new List<ModelError>();
            ModelState.Values.ToList().ForEach(item => errorList.AddRange(item.Errors.ToList()));
            errorList.ForEach(item => message += item.ErrorMessage + "\n");
            return Json(new { Status = false, Message = message });
        }

        public ActionResult Details(Guid id)
        {
            var model = this._IF_CreditCardApplicationService.GetByKey(id);
            return View(model);
        }

        public ActionResult Delete(string [] ids)
        {
            var list = new List<F_CreditCardApplicationDTO>();
            foreach(var item in ids)
            {
                list.Add(new F_CreditCardApplicationDTO { Id = Guid.Parse(item) });
            }
            this._IF_CreditCardApplicationService.Delete(list);
            return RedirectToAction("Index");
        }
    }
}