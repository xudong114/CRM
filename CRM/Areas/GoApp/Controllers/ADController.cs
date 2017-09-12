using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GoApp.Controllers
{
    public class ADController : BaseController
    {
        private readonly IF_ADService _IF_ADService;
        public ADController(IF_ADService iF_ADService)
        {
            this._IF_ADService = iF_ADService;
        }

        // GET: GoApp/AD
        public ActionResult Index(string code = "")
        {
            var list = this._IF_ADService.GetAll(code);

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(F_ADDTO ad)
        {
            if (ModelState.IsValid)
            {
                this._IF_ADService.Create(ad);
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Edit(Guid id)
        {
            var model = this._IF_ADService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(F_ADDTO ad)
        {
            if(ModelState.IsValid)
            {
                this._IF_ADService.Update(new List<F_ADDTO> { ad });
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(Guid id)
        {
            this._IF_ADService.Delete(new List<F_ADDTO> { new F_ADDTO { Id = id } });
            return RedirectToAction("index");
        }

    }
}