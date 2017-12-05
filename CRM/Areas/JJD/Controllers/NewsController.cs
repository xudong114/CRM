using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJD.Controllers
{
    [AllowAnonymous]
    public class NewsController : Controller
    {
        public NewsController() { }

        private readonly IG_NewsService _IG_NewsService;
        public NewsController(IG_NewsService iG_NewsService)
        {
            this._IG_NewsService = iG_NewsService;
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            var list = new List<G_NewsDTO>();
            list.AddRange(this._IG_NewsService.GetAll("活动"));
            list.AddRange(this._IG_NewsService.GetAll("新闻"));
            return View(list);
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult About()
        {
            var qa = this._IG_NewsService.GetAll("关于我们").FirstOrDefault() ?? new G_NewsDTO();
            return View(qa);
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Details(Guid id)
        {
            var news = this._IG_NewsService.GetByKey(id);
            return View(news);
        }
    }
}