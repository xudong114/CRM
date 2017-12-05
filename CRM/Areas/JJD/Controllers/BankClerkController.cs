using CRM.Areas.JJD.Models;
using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRM.Areas.JJD.Controllers
{
    public class BankClerkController : BaseController
    {
        public BankClerkController() { }
        private readonly IG_OrderService _IG_OrderService;
        private readonly IG_LoanProductService _IG_LoanProductService;
        private readonly IG_OrderRecordService _IG_OrderRecordService;

        public BankClerkController(IG_OrderService iG_OrderService,
            IG_LoanProductService iG_LoanProductService,
            IG_OrderRecordService iG_OrderRecordService) 
        {
            this._IG_OrderService = iG_OrderService;
            this._IG_LoanProductService = iG_LoanProductService;
            this._IG_OrderRecordService = iG_OrderRecordService;
        }

        // GET: JJD/BankManager
        public ActionResult Index()
        {
            var complexStatusCount = this._IG_OrderService.GetComplexStatusCount(this.User.Id, this.User.G_UserDetail.Code, Ingenious.Infrastructure.Enum.G_UserTypeEnum.BC);
            var model = new BankManagerComplexModel();

            model.PreProcessCount = complexStatusCount.GojiajuPassedCount;
            model.PreSignCount = complexStatusCount.BankPassedCount;
            model.PreSuccessedCount = complexStatusCount.BankSignedCount;
            model.SuccessedCount = complexStatusCount.SuccessedCount;
            model.PassedCount = complexStatusCount.CanceledCount +
                complexStatusCount.SignCanceledCount;

            model.User = this.User;
            model.MessageCount = 0;
            return View(model);
        }

        public ActionResult Orders(int pageIndex = 1, OrderFilterModel filter = null)
        {
            const int pageSize = 20;
            if (filter.IsFilter)
            {
                ViewBag.status = filter.Status.ToString().ToLower();
            }
            else
            {
                ViewBag.status = G_OrderBusinessStatusEnum.PreProcess.ToString().ToLower();
            }

            ViewBag.filter = filter.IsFilter;
            var list = this._IG_OrderService.GetAll(pageIndex, pageSize, null, this.GetStatus(filter), filter.ProductCode, filter.Month, filter.Keyword, null, null, this.User.G_UserDetail.Code);
            return View(list);
        }

        public ActionResult Filter()
        {
            var model = new OrderFilterModel();
            model.Products = this._IG_LoanProductService.GetAll();
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            this.DataBind();
            var model = this._IG_OrderService.GetComplexOrder(id);
            return View(model);
        }

        /// <summary>
        /// 订单审核
        /// </summary>
        /// <param name="id">订单标识（F_Order.Id）</param>
        /// <param name="status">订单状态：F_OrderStatusEnum</param>
        /// <param name="remark">审核备注</param>
        /// <returns></returns>
        public JsonResult UpdateOrder(Guid orderId, bool status = false, string remark = "")
        {
            var order = this._IG_OrderService.GetByKey(orderId);
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "无效订单" });
            }
            order.ModifiedBy = this.User.Id;

            if (order.Status == G_OrderStatusEnum.GojiajuPassed)
            {
                //1、银行通过审核
                if (status)
                {
                    order.Status = G_OrderStatusEnum.BankPassed;
                    //order.BankClerk = this.User.G_UserDetail.Code;
                    this._IG_OrderService.Update(new List<G_OrderDTO> { order });
                    this.CreateRecord(order.Id, "", G_OrderStatusEnum.BankPassed);
                    return Json(new MessageResult { Status = true, Message = "订单审核成功" }, JsonRequestBehavior.AllowGet);
                }
                else//2、银行取消申请
                {
                    order.Status = G_OrderStatusEnum.BankDenied;
                    //order.BankClerk = this.User.G_UserDetail.Code;
                    this._IG_OrderService.Update(new List<G_OrderDTO> { order });
                    this.CreateRecord(order.Id, "", G_OrderStatusEnum.BankDenied);
                    return Json(new MessageResult { Status = true, Message = "申请已取消" }, JsonRequestBehavior.AllowGet);
                }
            }

            if (order.Status == G_OrderStatusEnum.BankPassed)
            {
                //1、银行签约
                if (status)
                {
                    order.Status = G_OrderStatusEnum.BankSigned;
                    //order.BankClerk = this.User.G_UserDetail.Code;
                    this._IG_OrderService.Update(new List<G_OrderDTO> { order });
                    this.CreateRecord(order.Id, "", G_OrderStatusEnum.BankSigned);
                    return Json(new MessageResult { Status = true, Message = "签约成功" }, JsonRequestBehavior.AllowGet);
                }
                else//2、取消签约
                {
                    order.Status = G_OrderStatusEnum.SignCanceled;
                    //order.BankClerk = this.User.G_UserDetail.Code;
                    this._IG_OrderService.Update(new List<G_OrderDTO> { order });
                    this.CreateRecord(order.Id, "", G_OrderStatusEnum.SignCanceled);
                    return Json(new MessageResult { Status = true, Message = "签约已取消" }, JsonRequestBehavior.AllowGet);
                }
            }

            if (order.Status == G_OrderStatusEnum.BankSigned)
            {
                //1、确认放款
                if (status)
                {
                    order.Status = G_OrderStatusEnum.Successed;
                    //order.BankClerk = this.User.G_UserDetail.Code;
                    this._IG_OrderService.Update(new List<G_OrderDTO> { order });
                    this.CreateRecord(order.Id, "", G_OrderStatusEnum.Successed);
                    return Json(new MessageResult { Status = true, Message = "放款成功" }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new MessageResult { Status = false, Message = "订单状态错误" }, JsonRequestBehavior.AllowGet);
        }

        private void CreateRecord(Guid orderId, string remark, G_OrderStatusEnum status)
        {
            var orderRecord = new G_OrderRecordDTO
            {
                CreatedBy = this.User.Id,
                ModifiedBy = this.User.Id,
                Status = status,
                OrderId = orderId,
                Remark = remark,
                UserId = this.User.Id,
            };

            this._IG_OrderRecordService.Create(orderRecord);
        }

        /// <summary>
        /// 根据业务状态获取订单状态列表
        /// </summary>
        /// <param name="status">业务状态</param>
        /// <returns></returns>
        private List<G_OrderStatusEnum> GetStatus(OrderFilterModel filter)
        {
            var list = new List<G_OrderStatusEnum>();
            if (!filter.IsFilter)
                return list;
            switch (filter.Status)
            {
                case G_OrderBusinessStatusEnum.Canceled:
                    {
                        list.Add(G_OrderStatusEnum.Canceled);
                        list.Add(G_OrderStatusEnum.SignCanceled);
                    }
                    break;
                case G_OrderBusinessStatusEnum.PreProcess:
                    {
                        list.Add(G_OrderStatusEnum.GojiajuPassed);
                    }
                    break;
                case G_OrderBusinessStatusEnum.PreSign:
                    {
                        list.Add(G_OrderStatusEnum.BankPassed);
                    }
                    break;
                case G_OrderBusinessStatusEnum.PreSuccess:
                    {
                        list.Add(G_OrderStatusEnum.BankSigned);
                    }
                    break;
                case G_OrderBusinessStatusEnum.Successed:
                    {
                        list.Add(G_OrderStatusEnum.Successed);
                    }
                    break;
                case G_OrderBusinessStatusEnum.Other:
                    {
                        //list.Add(G_OrderStatusEnum.Canceled);
                        //list.Add(G_OrderStatusEnum.SignCanceled);
                        //list.Add(G_OrderStatusEnum.InProcess);
                        //list.Add(G_OrderStatusEnum.BankPassed);
                        //list.Add(G_OrderStatusEnum.BankSigned);
                        //list.Add(G_OrderStatusEnum.Successed);
                    }
                    break;
            }

            return list;
        }

        private void DataBind()
        {

        }
    }
}