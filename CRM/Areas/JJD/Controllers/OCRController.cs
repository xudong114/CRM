using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJD.Controllers
{
    public class OCRController : BaseController
    {
        // GET: JJD/OCR
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取身份证信息
        /// </summary>
        /// <param name="filename">图片文件路径</param>
        /// <returns></returns>
        public JsonResult GetIdcardInfo(string filename)
        {
            var result = Ingenious.Infrastructure.Helper.OCRHelper.GetIdcardInfoUrl(filename);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取营业执照信息
        /// </summary>
        /// <param name="filename">图片文件路径</param>
        /// <returns></returns>
        public JsonResult GetBusinessLicenseInfo(string filename)
        {
            var result = Ingenious.Infrastructure.Helper.OCRHelper.GetBusinessLicenseInfo(filename);

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
    }
}