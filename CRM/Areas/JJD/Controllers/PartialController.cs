using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJD.Controllers
{
    public class PartialController : Controller
    {
        // GET: JJD/Partial
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Navbar()
        {
            
            return PartialView();
        }

    }
}