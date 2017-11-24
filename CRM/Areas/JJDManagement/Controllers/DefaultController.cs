using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJDManagement.Controllers
{
    public class DefaultController : Controller
    {
        // GET: JJDManagement/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}