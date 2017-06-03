using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.ManagementCenter.Controllers
{
    public class HomeController : BaseController
    {
        // GET: ManagementCenter/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}