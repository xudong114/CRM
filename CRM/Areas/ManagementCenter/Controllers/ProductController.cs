using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.ManagementCenter.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _IProductService;
        public ProductController(
            IProductService iProductService)
        {
            this._IProductService = iProductService;
        }

        // GET: ManagementCenter/Product
        public ActionResult Index()
        {
            var products = this._IProductService.GetAll();
            return View(products);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductDTO product)
        {
            if(ModelState.IsValid)
            {
                product.CreatedBy = product.ModifiedBy = this.User.Id;
                this._IProductService.Create(product);
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public ActionResult Edit(Guid id)
        {
            var model = this._IProductService.GetByKey(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                product.ModifiedBy = this.User.Id;
                this._IProductService.Update(new ProductDTOList { product });
                return RedirectToAction("Index");
            }

            return View(product);
        }

    }
}