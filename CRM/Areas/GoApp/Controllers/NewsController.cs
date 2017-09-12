using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GoApp.Controllers
{
    public class NewsController : BaseController
    {
        public NewsController() { }

        private readonly IF_NewsService _IF_NewsService;
        public NewsController(IF_NewsService iF_NewsService)
        {
            this._IF_NewsService = iF_NewsService;
        }

        // GET: GoApp/News
        public ActionResult Index(string code="",string title="")
        {
            var list = this._IF_NewsService.GetAll(code,title);
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(F_NewsDTO news)
        {
            if (ModelState.IsValid)
            {
                this._IF_NewsService.Create(news);
            }

            return RedirectToAction("index");
        }

        public ActionResult Edit(Guid id)
        {
            var news = this._IF_NewsService.GetByKey(id);
            return View(news);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(F_NewsDTO news)
        {
            if (ModelState.IsValid)
            {
                this._IF_NewsService.Update(new List<F_NewsDTO> { news });
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(Guid id)
        {
            this._IF_NewsService.Delete(new List<F_NewsDTO> { new F_NewsDTO { Id = id } });
            return RedirectToAction("index");
        }

    }
}