using CRM.Areas.ManagementCenter.Models;
using Ingenious.Application.Interface;
using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.ManagementCenter.Controllers
{
    public class PriceStrategyController : BaseController
    {
        private readonly IPriceStrategyService _IPriceStrategyService;
        private readonly IPriceStrategyDetailService _IPriceStrategyDetailService;
        private readonly IProductService _IProductService;
        public PriceStrategyController(
            IPriceStrategyService iPriceStrategyService,
            IProductService iProductService,
            IPriceStrategyDetailService iPriceStrategyDetailService)
        {
            this._IPriceStrategyService = iPriceStrategyService;
            this._IProductService = iProductService;
            this._IPriceStrategyDetailService = iPriceStrategyDetailService;
        }

        // GET: ManagementCenter/PriceStrategy
        public ActionResult Index()
        {
            var priceStrategies = this._IPriceStrategyService.GetAll();
            return View(priceStrategies);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(PriceStrategyDTO priceStrategy)
        {
            if (ModelState.IsValid)
            {
                priceStrategy.CreatedBy = priceStrategy.ModifiedBy = this.User.Id;
                this._IPriceStrategyService.Create(priceStrategy);
                return RedirectToAction("Index");
            }
            return View(priceStrategy);
        }

        public ActionResult Detail(Guid id)
        {
            var model = new PriceStrategyComposite();
            model.PriceStrategyDTO = this._IPriceStrategyService.GetByKey(id);
            model.ProductDTOList = this._IProductService.GetAll();
            return View(model);
        }
        /// <summary>
        /// 修改价格明细
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(PriceStrategyComposite composite)
        {
            composite.PriceStrategyDetailDTOList.ForEach(item =>
            {
                item.ModifiedBy = item.CreatedBy = this.User.Id;
                item.CreatedDate = item.ModifiedDate = DateTime.Now;
            });
            this._IPriceStrategyDetailService.Update(composite.PriceStrategyDetailDTOList);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 初次新建价格明细
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Submit(PriceStrategyComposite composite)
        {
            composite.PriceStrategyDetailDTOList.ForEach(item =>
                {
                    item.ModifiedBy = item.CreatedBy = this.User.Id;
                    item.CreatedDate = item.ModifiedDate = DateTime.Now;
                });
            this._IPriceStrategyDetailService.Create(composite.PriceStrategyDetailDTOList);
            //return RedirectToAction("Detail",composite.PriceStrategyDetailDTOList.First().PriceStrategyId);
            return RedirectToAction("Index");
        }
    }
}
