using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Go.Controllers
{
    [AllowAnonymous]
    public class NewsController : BaseController
    {
        public NewsController() { }

        private readonly IF_NewsService _IF_NewsService;
        public NewsController(IF_NewsService iF_NewsService)
        {
            this._IF_NewsService = iF_NewsService;
        }
        /// <summary>
        /// 获取QA
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult QAS()
        {
            var list = this._IF_NewsService.GetAll("QA");
            return Json(new MessageResult { Status = true, Data = list });
        }

        /// <summary>
        /// 获取QA详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult QA(Guid id)
        {
            var qa = this._IF_NewsService.GetByKey(id);
            return Json(new MessageResult { Status = true, Data = qa });
        }

        /// <summary>
        /// 关于我们
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult About()
        {
            var qa = this._IF_NewsService.GetAll("关于我们").FirstOrDefault()??new F_NewsDTO();
            return Json(new MessageResult { Status = true, Data = qa });
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult News(string code)
        {
            var list = this._IF_NewsService.GetAll(code);
            return Json(new MessageResult { Status = true, Data = list });
        }

        /// <summary>
        /// 获取新闻列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Details(Guid id)
        {
            var news = this._IF_NewsService.GetByKey(id);
            return Json(new MessageResult { Status = true, Data = news });
        }
    }
}
