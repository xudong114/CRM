using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJDManagement.Controllers
{
    public class ProductController : BaseController
    {
        public ProductController() { }
        private readonly IG_LoanProductService _IG_LoanProductService;
        public ProductController(IG_LoanProductService iG_LoanProductService)
        {
            this._IG_LoanProductService = iG_LoanProductService;
        }

        // GET: JJDManagement/Product
        public ActionResult Index()
        {
            var list = this._IG_LoanProductService.GetAll();

            return View(list);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(G_LoanProductDTO product)
        {
            if(ModelState.IsValid)
            {
                this._IG_LoanProductService.Create(product);
            }
            
            return RedirectToAction("index");
        }
        public ActionResult Edit(Guid id)
        {
            var model = this._IG_LoanProductService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(G_LoanProductDTO product)
        {
            if (ModelState.IsValid)
            {
                this._IG_LoanProductService.Update(new List<G_LoanProductDTO> { product });
            }

            return RedirectToAction("index");
        }


    }
}