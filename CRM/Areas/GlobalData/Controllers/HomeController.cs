using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GlobalData.Controllers
{
    public class HomeController : BaseController
    {
        // GET: GlobalData/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}