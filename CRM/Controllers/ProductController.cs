using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService _IProductService;
        private readonly IPriceStrategyService _IPriceStrategyService;
        public ProductController(
            IProductService iProductService,
            IPriceStrategyService iPriceStrategyService)
        {
            this._IProductService = iProductService;
            this._IPriceStrategyService = iPriceStrategyService;
        }

        // GET: ManagementCenter/Product
        public ActionResult Index()
        {
            var priceStrategy = this._IPriceStrategyService.GetPriceStrategyByUserId(this.User.Id);
            var products = this._IProductService.GetAll();
            if (priceStrategy != null)
            {
                foreach (var item in products)
                {
                    var p = priceStrategy.PriceStrategyDetailList.Find(model => model.ProductId == item.Id);
                    if(p!=null)
                    {
                        item.Price = p.Price;
                        item.RenewPrice = p.RenewPrice;
                        item.DiscountRate = p.DiscountRate;
                        item.Minimum = p.Minimum;
                    }
                }
            }
            return View(products);
        }
    }
}