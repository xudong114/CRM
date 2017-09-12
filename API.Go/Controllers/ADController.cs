using Ingenious.Application.Interface;
using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Go.Controllers
{
    public class ADController : BaseController
    {
        public ADController() { }
        private readonly IF_ADService _IF_ADService;

        public ADController(IF_ADService iF_ADService)
        {
            this._IF_ADService = iF_ADService;
        }

        /// <summary>
        /// 获取广告位
        /// </summary>
        /// <param name="code">广告位分类</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetAD(string code = "")
        {
            var ads = this._IF_ADService.GetAll(code);

            return Json(new MessageResult { Status = true, Data = ads });
        }

        /// <summary>
        /// 获取首页轮播图广告位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Top()
        {
            var ads = this._IF_ADService.GetAll("首页轮播图");

            return Json(new MessageResult { Status = true, Data = ads });
        }
        /// <summary>
        /// 获取首页广告位
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Home()
        {
            var ads = this._IF_ADService.GetAll(new string[] { "首页轮播图", "轮播图下方", "立即申请下方" });

            return Json(new MessageResult { Status = true, Data = ads });
        }

    }
}
