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
    /// <summary>
    /// 金融客服
    /// </summary>
    public class ClientServiceController : BaseController
    {
        public ClientServiceController() { }

        private readonly IG_OrderService _IG_OrderService;
        private readonly IG_LoanProductService _IG_LoanProductService;
        private readonly IG_ActivityService _IG_ActivityService;
        private readonly IG_UserDetailService _IG_UserDetailService;
        private readonly IG_UserService _IG_UserService;
        private readonly IG_OrderRecordService _IG_OrderRecordService;
        private readonly IG_BankService _IG_BankService;

        public ClientServiceController(IG_OrderService iG_OrderService,
            IG_LoanProductService iG_LoanProductService,
            IG_ActivityService iG_ActivityService,
            IG_UserDetailService iG_UserDetailService,
            IG_OrderRecordService iG_OrderRecordService,
            IG_BankService iG_BankService,
            IG_UserService iG_UserService)
        {
            this._IG_OrderService = iG_OrderService;
            this._IG_LoanProductService = iG_LoanProductService;
            this._IG_ActivityService = iG_ActivityService;
            this._IG_UserDetailService = iG_UserDetailService;
            this._IG_OrderRecordService = iG_OrderRecordService;
            this._IG_BankService = iG_BankService;
            this._IG_UserService = iG_UserService;
        }

        // GET: JJD/ClientManager
        public ActionResult Index()
        {
            var complexStatusCount = this._IG_OrderService.GetComplexStatusCount(this.User.Id, this.User.G_UserDetail.Code, Ingenious.Infrastructure.Enum.G_UserTypeEnum.CS);
            var model = new ClientServiceComplexModel();

            model.PreProcessCount = complexStatusCount.PreProcess;
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
            ViewBag.status = filter.Status.ToString().ToLower();
            ViewBag.filter = filter.IsFilter;
            var list = this._IG_OrderService.GetAll(pageIndex, pageSize, null, this.GetStatus(filter.Status), filter.ProductCode, filter.Month, filter.Keyword, null, null, filter.BankClerkCode, null, this.User.G_UserDetail.Code);
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
            var model = this._IG_OrderService.GetComplexOrder(id);
            return View(model);
        }

        public ActionResult ReadonlyDetails(Guid id)
        {
            var model = this._IG_OrderService.GetComplexOrder(id);
            return View(model);
        }
        

        public JsonResult UpdateOrder(Guid orderId, string remark, bool status = false)
        {
            var order = this._IG_OrderService.GetByKey(orderId);
            if (order != null)
            {
                //1、如果审核通过，则提交到金融经理
                if (status)
                {
                    order.Status = G_OrderStatusEnum.InProcess;

                    var manager = this._IG_UserService.GetGojiajuManagerByEntityId(this.User.G_EntityId.Value);
                    if (manager != null)
                    {
                        this.CreateRecord(order, G_OrderStatusEnum.PrePassed, remark);
                        this.CreateRecord(order, G_OrderStatusEnum.InProcess);
                        order.GoJiajuManagerCode = manager.G_UserDetail.Code;
                    }
                    else
                    {
                        return Json(new MessageResult { Status = false, Message = "当前机构不存在【金融经理】账号" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    this.CreateRecord(order, G_OrderStatusEnum.PreDenied, remark);
                    order.Status = G_OrderStatusEnum.PreDenied;
                }

                this._IG_OrderService.Update(new List<Ingenious.DTO.G_OrderDTO> { order });
                return Json(new MessageResult { Status = true, Message = status ? "提交成功" : "订单已取消" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageResult { Status = false, Message = "订单无效" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 根据业务状态获取订单状态列表
        /// </summary>
        /// <param name="status">业务状态</param>
        /// <returns></returns>
        private List<G_OrderStatusEnum> GetStatus(G_OrderBusinessStatusEnum status = G_OrderBusinessStatusEnum.Other)
        {
            var list = new List<G_OrderStatusEnum>();
            switch (status)
            {
                case G_OrderBusinessStatusEnum.Canceled:
                    {
                        list.Add(G_OrderStatusEnum.Canceled);
                        list.Add(G_OrderStatusEnum.SignCanceled);
                    }
                    break;
                case G_OrderBusinessStatusEnum.PreProcess:
                    {
                        list.Add(G_OrderStatusEnum.PreProcess);
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

        private void CreateRecord(G_OrderDTO order,G_OrderStatusEnum status,string remark="")
        {
            var record = new G_OrderRecordDTO();
            record.OrderId = order.Id;
            record.CreatedBy = record.ModifiedBy = this.User.Id;
            record.CreatedDate = record.ModifiedDate = DateTime.Now;
            record.Remark = remark;
            record.Status = status;
            record.UserCode = this.User.G_UserDetail.Code;
            record.UserId = this.User.Id;
            this._IG_OrderRecordService.Create(record);
        }

        private void DataBind()
        {
            var banks = this._IG_BankService.GetBanks();
            //var list = new SelectList(banks, "Code", "Name");
            ViewBag.Banks = banks;

            //var entities = this._IG_EntityService.GetAll(1, int.MaxValue);
            //ViewBag.entities = new SelectList(entities.Data, "Id", "EntityName");
        }

    }
}