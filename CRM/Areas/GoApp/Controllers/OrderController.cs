using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.GoApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IF_OrderService _IF_OrderService;
        public OrderController(IF_OrderService iF_OrderService)
        {
            this._IF_OrderService = iF_OrderService;
        }

        // GET: GoApp/Order
        public ActionResult Index(int page = 1, string sort = "createddate_desc")
        {
            const int pageSize = 20;
            var list = this._IF_OrderService.GetAll(page, pageSize, null, null, null, null, null, null, null, null, null, null, null, null, true,sort);
            return View(list);
        }

        public JsonResult Delete(Guid id)
        {
            this._IF_OrderService.Delete(new List<Ingenious.DTO.F_OrderDTO> { new F_OrderDTO { Id = id, IsActive = false } });
            return Json(new MessageResult { Status = true, Message = "删除成功" }, JsonRequestBehavior.AllowGet);
        }

    }
}