using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GoApp.Controllers
{
    public class FilesController : Controller
    {
        public ActionResult CRM()
        {
            return Upload();
        }

        public ActionResult App()
        {
            return Upload("app");
        }

        public ActionResult JJD()
        {
            return Upload("jiajudai");
        }

        [AllowAnonymous]
        public ActionResult Creditcard()
        {
            return Upload("creditcard");
        }

        public ActionResult Upload(string floder = "crm")
        {
            string domain = ConfigurationManager.AppSettings.Get("domain");// "http://crm.gojiaju.cn/";
            const int maxSize = 3 * 1024 * 1024; //3M
            var hash = new Hashtable();
            //HttpPostedFileBase file = Request.Files[0];

            string topPath = string.Format("/upload/{0}/", floder);
            string path = string.Format("/{0}/{1}/{2}/",
                DateTime.Now.ToString("yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                DateTime.Now.ToString("MM", System.Globalization.DateTimeFormatInfo.InvariantInfo),
                DateTime.Now.ToString("dd", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                );
            string complexPath = topPath + path;
            string root = System.Web.HttpContext.Current.Server.MapPath(complexPath);

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                if (file != null)
                {
                    int fileLen = file.ContentLength; //获取上传文件的大小
                    if (fileLen <= maxSize)
                    {
                        var digtName = DateTime.Now.Ticks.ToString();
                        digtName = string.Format("{0}_{1}", digtName, file.FileName);

                        file.SaveAs(Path.Combine(root, digtName));
                        hash["error"] = 0;
                        hash["url"] = domain + Path.Combine(complexPath, digtName);
                        hash["message"] = "";
                    }
                    else
                    {
                        hash["error"] = 1;
                        hash["message"] = "您上传的文件超出限制,最大文件为3M，请处理后上传。";
                    }
                }
            }

            return Json(hash, JsonRequestBehavior.AllowGet);
        }
    }

    public class FileResultMessage
    {
        public bool success { get; set; }

        public string message { get; set; }

        public string FilePath { get; set; }
    }
}
