using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJDManagement.Controllers
{
    public class ADController : BaseController
    {
        private readonly IG_ADService _IG_ADService;
        public ADController(IG_ADService IG_ADService)
        {
            this._IG_ADService = IG_ADService;
        }

        // GET: GoApp/AD
        public ActionResult Index(string code = "")
        {
            var list = this._IG_ADService.GetAll(code);

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(G_ADDTO ad)
        {
            if (ModelState.IsValid)
            {
                this._IG_ADService.Create(ad);
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Edit(Guid id)
        {
            var model = this._IG_ADService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(G_ADDTO ad)
        {
            if (ModelState.IsValid)
            {
                this._IG_ADService.Update(new List<G_ADDTO> { ad });
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(Guid id)
        {
            this._IG_ADService.Delete(new List<G_ADDTO> { new G_ADDTO { Id = id } });
            return RedirectToAction("index");
        }

    }
}