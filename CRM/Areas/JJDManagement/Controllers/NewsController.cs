using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJDManagement.Controllers
{
    public class NewsController : BaseController
    {
        public NewsController() { }

        private readonly IG_NewsService _IG_NewsService;
        public NewsController(IG_NewsService iG_NewsService)
        {
            this._IG_NewsService = iG_NewsService;
        }

        // GET: GoApp/News
        public ActionResult Index(string code = "", string title = "")
        {
            var list = this._IG_NewsService.GetAll(code, title);
            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(G_NewsDTO news)
        {
            if (ModelState.IsValid)
            {
                this._IG_NewsService.Create(news);
            }

            return RedirectToAction("index");
        }

        public ActionResult Edit(Guid id)
        {
            var news = this._IG_NewsService.GetByKey(id);
            return View(news);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(G_NewsDTO news)
        {
            if (ModelState.IsValid)
            {
                this._IG_NewsService.Update(new List<G_NewsDTO> { news });
            }
            return RedirectToAction("index");
        }

        public ActionResult Delete(Guid id)
        {
            this._IG_NewsService.Delete(new List<G_NewsDTO> { new G_NewsDTO { Id = id } });
            return RedirectToAction("index");
        }

    }
}