using CRM.Areas.JJD.Models;
using Ingenious.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJD.Controllers
{
    /// <summary>
    /// 消费者后台
    /// </summary>
    public class ConsumerController : BaseController
    {
        public ConsumerController() { }

        private readonly IG_OrderService _IG_OrderService;
        public ConsumerController(IG_OrderService iG_OrderService)
        {
            this._IG_OrderService = iG_OrderService;
        }

        // GET: JJD/Cunsomer
        public ActionResult Index()
        {
            var complexStatusCount =  this._IG_OrderService.GetComplexStatusCount(this.User.Id);
            var model = new BackgroundIndexComplexModel();

            model.TempCount = complexStatusCount.TempCount;
            model.InProcessCount = complexStatusCount.InProcessCount
                + complexStatusCount.PreProcess;

            model.PassedCount = complexStatusCount.SignCanceledCount
                +complexStatusCount.CanceledCount;
            model.SuccessedCount = complexStatusCount.SuccessedCount;

            model.User = this.User;
            model.MessageCount = 0;
            return View(model);
        }

        public ActionResult Orders(int pageIndex = 1, string status = "inprocess")
        {
            ViewBag.status = status;
            const int pageSize = 20;
            var list = this._IG_OrderService.GetAll(pageIndex, pageSize, this.User.Id);
            return View(list);
        }

        public ActionResult Details(Guid id)
        {
            var order = this._IG_OrderService.GetComplexOrder(id);
            return View(order);
        }
    }
}