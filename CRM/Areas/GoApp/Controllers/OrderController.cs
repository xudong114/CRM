using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GoApp.Controllers
{
    public class OrderController : BaseController
    {
        // GET: GoApp/Order
        public ActionResult Index()
        {
            return View();
        }
    }
}