using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GoApp.Controllers
{
    public class HomeController : BaseController
    {
        // GET: GoApp/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}