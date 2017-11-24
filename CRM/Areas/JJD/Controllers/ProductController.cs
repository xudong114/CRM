using Ingenious.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJD.Controllers
{
    public class ProductController : Controller
    {
        public ProductController() { }
        private readonly IG_LoanProductService _IG_LoanProductService;
        public ProductController(IG_LoanProductService iG_LoanProductService)
        {
            this._IG_LoanProductService = iG_LoanProductService;
        }

        // GET: JJD/Product
        public ActionResult Index()
        {
            var list = this._IG_LoanProductService.GetAll();
            return View(list);
        }

        public ActionResult Details(Guid id)
        {
            var model = this._IG_LoanProductService.GetByKey(id);
            return View(model);
        }

    }
}