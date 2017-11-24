using Ingenious.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GoApp.Controllers
{
    public class StoreController : BaseController
    {
        public StoreController() { }

        private readonly IF_StoreService _IF_StoreService;
        public StoreController(IF_StoreService iF_StoreService) 
        {
            this._IF_StoreService = iF_StoreService;
        }

        // GET: GoApp/Store
        public ActionResult Index()
        {
            var list = this._IF_StoreService.GetStores();
            return View(list);
        }

    }
}