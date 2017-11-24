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
    /// 金融经理
    /// </summary>
    public class ClientManagerController : BaseController
    {
        public ClientManagerController() { }

        private readonly IG_OrderService _IG_OrderService;
        private readonly IG_LoanProductService _IG_LoanProductService;
        private readonly IG_ActivityService _IG_ActivityService;
        private readonly IG_UserDetailService _IG_UserDetailService;
        private readonly IG_UserService _IG_UserService;
        private readonly IG_OrderRecordService _IG_OrderRecordService;
        private readonly IG_BankService _IG_BankService;

        public ClientManagerController(IG_OrderService iG_OrderService,
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
            var complexStatusCount = this._IG_OrderService.GetComplexStatusCount(this.User.Id, this.User.G_UserDetail.Code, Ingenious.Infrastructure.Enum.G_UserTypeEnum.CL);
            var model = new ClientManagerComplexModel();

            model.PreProcessCount = complexStatusCount.InProcessCount;
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
            var list = this._IG_OrderService.GetAll(pageIndex, pageSize, null, this.GetStatus(filter), filter.ProductCode, filter.Month, filter.Keyword, null, null, filter.BankClerkCode, this.User.G_UserDetail.Code);
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
        public JsonResult UpdateOrder(Guid orderId, string bankCode = "", string remark = "")
        {
            var order = this._IG_OrderService.GetByKey(orderId);
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "无效订单" });
            }
            order.ModifiedBy = this.User.Id;

            //1、拒绝分配银行
            if (string.IsNullOrWhiteSpace(bankCode))
            {
                order.Status = G_OrderStatusEnum.GojiajuDenied;
                this._IG_OrderService.Update(new List<G_OrderDTO> { order });
                this.CreateRecord(order.Id, "", G_OrderStatusEnum.GojiajuPassed);
            }
            else
            {
                order.Status = G_OrderStatusEnum.GojiajuPassed;
                order.BankCode = bankCode;
                var bank = this._IG_BankService.GetBank(bankCode);
                var bankUsers = this._IG_BankService.GetUserByBank(bankCode);

                var bankManager = bankUsers.Where(item => item.G_User.UserType == G_UserTypeEnum.BM).FirstOrDefault();

                //2、分配银行 信贷经理
                //if(!bank.IsAssignAuto)
                //{
                if (bankManager != null)
                {
                    order.BankManager = bankManager.Code;
                }
                else
                {
                    return Json(new MessageResult { Status = false, Message = "当前银行未分配【信贷经理】账号" }, JsonRequestBehavior.AllowGet);
                }
                //}

                //3、分配银行 客户经理（开启自动分配）
                if (bank.IsAssignAuto)
                {
                    var bankClerk = this._IG_BankService.AssignOrderToClerk(bankCode);
                    if (bankClerk != null)
                    {
                        order.BankClerk = bankClerk;
                        this._IG_OrderService.Update(new List<G_OrderDTO> { order });
                        this.CreateRecord(order.Id, "", G_OrderStatusEnum.GojiajuPassed);
                    }
                    else
                    {
                        return Json(new MessageResult { Status = false, Message = "当前银行未分配【客户经理】账号" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    this._IG_OrderService.Update(new List<G_OrderDTO> { order });
                    this.CreateRecord(order.Id, "", G_OrderStatusEnum.GojiajuPassed);
                }
            }

            return Json(new MessageResult { Status = true, Message = "分配银行成功" }, JsonRequestBehavior.AllowGet);
        }

        private void CreateRecord(Guid orderId,string remark,G_OrderStatusEnum status)
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

        public JsonResult AssignOrder(Guid orderId, string clerkCode)
        {
            if (string.IsNullOrWhiteSpace(clerkCode))
            {
                return Json(new MessageResult { Status = false, Message = "未分配客服经理" });
            }
            if (this.User.UserType != G_UserTypeEnum.BM)
            {
                return Json(new MessageResult { Status = false, Message = GlobalMessage.Unauthorized });
            }
            var order = this._IG_OrderService.GetByKey(orderId);
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "订单无效" });
            }

            order.BankClerk = clerkCode;
            this._IG_OrderService.Update(new List<G_OrderDTO> { order });

            return Json(new MessageResult { Status = true, Message = "订单分配成功" });
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
                        list.Add(G_OrderStatusEnum.InProcess);
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

        private void CreateMessage(G_OrderDTO order, G_OrderRecordDTO record)
        {
            var message = new G_ActivityDTO();

            switch (order.Status)
            {
                case G_OrderStatusEnum.InProcess:
                    {
                        message.Title = "您有一笔新订单提交成功！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：待审核", order.Id, order.Code);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = this.User.Id;
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        var user = this._IG_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔新订单待处理！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：待审核", order.Id, order.Code);
                            message.ReferenceId = user.G_UserId;
                            this._IG_ActivityService.Create(message);
                        }
                    }
                    break;
                case G_OrderStatusEnum.GojiajuDenied:
                    {
                        message.Title = "您有一笔订单未通过平台审核！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        message.Title = "您拒绝了一笔订单申请！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台拒绝", order.Id, order.Code);
                        message.ReferenceId = this.User.Id;
                        this._IG_ActivityService.Create(message);
                    }
                    break;
                case G_OrderStatusEnum.GojiajuPassed:
                    {
                        message.Title = "您有一笔订单通过平台审核！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台通过<br/>", order.Id, order.Code);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        message.Title = "您通过了一笔订单申请！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台通过", order.Id, order.Code);
                        message.ReferenceId = this.User.Id;//平台
                        this._IG_ActivityService.Create(message);

                        var bankManager = this._IG_UserDetailService.GetUserDetailByCode(order.BankManager);
                        if (bankManager != null)
                        {
                            message.Title = "您有一笔订单待处理！";//银行信贷经理
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台通过", order.Id, order.Code);
                            message.ReferenceId = bankManager.G_UserId;
                            this._IG_ActivityService.Create(message);
                        }

                        var bankClient = this._IG_UserDetailService.GetUserDetailByCode(order.BankManager);
                        if (bankClient != null)
                        {
                            message.Title = "您有一笔订单待处理！";//银行客户经理
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台通过", order.Id, order.Code);
                            message.ReferenceId = bankManager.G_UserId;
                            this._IG_ActivityService.Create(message);
                        }
                    }
                    break;
                case G_OrderStatusEnum.BankDenied:
                    {
                        message.Title = "您有一笔订单未通过银行审核！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        message.Title = "您拒绝了一笔订单申请！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.ReferenceId = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        var user = this._IG_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单未通过银行审核！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                            message.ReferenceId = user.G_UserId;
                            this._IG_ActivityService.Create(message);
                        }
                    }
                    break;
                case G_OrderStatusEnum.BankPassed:
                    {
                        message.Title = "您有一笔订单通过银行审核！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行通过", order.Id, order.Code);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        message.Title = "您通过了一笔订单申请！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行通过", order.Id, order.Code);
                        message.ReferenceId = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        var user = this._IG_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单通过银行审核！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行通过", order.Id, order.Code);
                            message.ReferenceId = user.G_UserId;
                            this._IG_ActivityService.Create(message);
                        }
                    }
                    break;
                case G_OrderStatusEnum.BankSigned:
                    {
                        message.Title = "您有一笔订单签约成功！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        message.Title = "您签约了一笔订单申请！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.ReferenceId = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        var user = this._IG_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单签约成功！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                            message.ReferenceId = user.G_UserId;
                            this._IG_ActivityService.Create(message);
                        }
                    }
                    break;
                case G_OrderStatusEnum.SignCanceled:
                    {
                        message.Title = "您有一笔订单签约取消！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        message.Title = "您取消了一笔订单签约！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.ReferenceId = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        var user = this._IG_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单签约取消！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                            message.ReferenceId = user.G_UserId;
                            this._IG_ActivityService.Create(message);
                        }
                    }
                    break;
                case G_OrderStatusEnum.Successed:
                    {
                        message.Title = "您有一笔订单放款成功！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：已放款<br/>申请金额：{2}<br/>实际放款：{3}万", order.Id, order.Code, order.LoanAmount, order.GotLoanAmount);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        message.Title = "您有一笔订单放款成功！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：已放款<br/>申请金额：{2}<br/>实际放款：{3}万", order.Id, order.Code, order.LoanAmount, order.GotLoanAmount);
                        message.ReferenceId = this.User.Id;
                        this._IG_ActivityService.Create(message);

                        var user = this._IG_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单放款成功！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：已放款<br/>申请金额：{2}<br/>实际放款：{3}万", order.Id, order.Code, order.LoanAmount, order.GotLoanAmount);
                            message.ReferenceId = user.G_UserId;
                            this._IG_ActivityService.Create(message);
                        }
                    }
                    break;
                case G_OrderStatusEnum.Canceled:
                    {
                    }
                    break;

            }

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