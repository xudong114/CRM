using Ingenious.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.ManagementCenter.Controllers
{
    public class GoAPPController : Controller
    {
        private readonly IF_OrderService _IF_OrderService;
        public GoAPPController(IF_OrderService iF_OrderService)
        {
            this._IF_OrderService = iF_OrderService;
        }

        // GET: ManagementCenter/GoAPP
        public ActionResult Index()
        {
            return View();
        }
    }
}