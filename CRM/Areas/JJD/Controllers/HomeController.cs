using CRM.Areas.JJD.Models;
using Ingenious.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJD.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public HomeController() { }

        private readonly IG_ADService _IG_ADService;
        private readonly IG_LoanProductService _IG_LoanProductService;
        private readonly IG_OrderService _IG_OrderService;
        public HomeController(IG_ADService iG_ADService,
            IG_LoanProductService iG_LoanProductService,
            IG_OrderService iG_OrderService)
        {
            this._IG_ADService = iG_ADService;
            this._IG_LoanProductService = iG_LoanProductService;
            this._IG_OrderService = iG_OrderService;
        }

        public ActionResult Index()
        {
            var model = new IndexComplexModel();
            model.ADs = this._IG_ADService.GetAll("");
            model.Products = this._IG_LoanProductService.GetAll();
            model.LatestOrders = this._IG_OrderService.GetAll(1, 10);
            return View(model);
        }

    }
}