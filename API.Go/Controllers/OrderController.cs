using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Cache;
using Ingenious.Infrastructure.Enum;
using Ingenious.Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Go.Controllers
{
    /// <summary>
    /// 管理申请单服务
    /// </summary>
    public class OrderController : BaseController
    {
        public OrderController() { }
        private readonly IF_ActivityService _IF_ActivityService;
        private readonly IF_BankService _IF_BankService;
        private readonly IF_UserService _IF_UserService;
        private readonly IF_StoreService _IF_StorerService;
        private readonly IF_OrderService _IF_OrderService;
        private readonly IF_FileService _IF_FileService;
        private readonly IF_OrderRecordService _IF_OrderRecordService;
        private readonly IF_UserDetailService _IF_UserDetailService;
        private readonly IF_WithdrawDepositRecordService _IF_WithdrawDepositRecordService;
        public OrderController(IF_OrderService iF_OrderService,
            IF_OrderRecordService iF_OrderRecordService,
            IF_FileService iF_FileService,
            IF_UserDetailService iF_UserDetailService,
            IF_WithdrawDepositRecordService iF_WithdrawDepositRecordService,
            IF_StoreService iF_StorerService,
            IF_BankService iF_BankService,
            IF_UserService iF_UserService,
            IF_ActivityService iActivityService)
        {
            this._IF_OrderService = iF_OrderService;
            this._IF_OrderRecordService = iF_OrderRecordService;
            this._IF_FileService = iF_FileService;
            this._IF_UserDetailService = iF_UserDetailService;
            this._IF_WithdrawDepositRecordService = iF_WithdrawDepositRecordService;
            this._IF_StorerService = iF_StorerService;
            this._IF_BankService = iF_BankService;
            this._IF_UserService = iF_UserService;
            this._IF_ActivityService = iActivityService;
        }

        /// <summary>
        /// 获取订单数据
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <returns></returns>
        public IHttpActionResult GetOrder(Guid id)
        {
            var order = this._IF_OrderService.GetByKey(id);
            //return Json(order);
            return Json(new MessageResult { Status = true, Data = order });
        }

        /// <summary>
        /// 获取订单所有信息
        /// 包括订单审核信息，状态信息
        /// </summary>
        /// <param name="orderId">订单Id</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetComplexOrder(Guid orderId)
        {
            var order = this._IF_OrderService.GetComplexOrder(orderId);
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "订单无效" });
            }
            var recordList = new List<F_OrderRecordDTO>();
            if (order.OrderRecordList.Count() > 0)
            {
                recordList.Add(order.OrderRecordList.Where(item =>
                      item.Status == F_OrderStatusEnum.GojiajuDenied
                      || item.Status == F_OrderStatusEnum.GojiajuPassed
                      ).OrderByDescending(item => item.CreatedDate).FirstOrDefault());

                recordList.Add(order.OrderRecordList.Where(item =>
                      item.Status == F_OrderStatusEnum.BankPassed
                      || item.Status == F_OrderStatusEnum.BankDenied
                      ).OrderByDescending(item => item.CreatedDate).FirstOrDefault());

                recordList.Add(order.OrderRecordList.Where(item =>
                      item.Status == F_OrderStatusEnum.BankSigned
                      || item.Status == F_OrderStatusEnum.SignCanceled
                      ).OrderByDescending(item => item.CreatedDate).FirstOrDefault());

                recordList.Add(order.OrderRecordList.Where(item =>
                      item.Status == F_OrderStatusEnum.Successed
                      ).OrderByDescending(item => item.CreatedDate).FirstOrDefault());

                recordList.RemoveAll(item => item == null);
                order.OrderRecordList = recordList;
            }

            return Json(new MessageResult { Status = true, Data = order });
        }

        /// <summary>
        /// 01：填写个人信息
        /// </summary>
        /// <param name="order">订单参数</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Step01(F_OrderDTO order)
        {
            if (order == null
                || string.IsNullOrWhiteSpace(order.Name)
                || string.IsNullOrWhiteSpace(order.IDNo))
            {
                return Json(new MessageResult { Status = false, Message = "资料不全" });
            }

            //var oldCode = APICacheService.Instance.Get(order.PersonalPhone, order.PersonalPhone);
            //if (oldCode == null)
            //{
            //    return Json(new MessageResult { Status = false, Message = "验证码失效" });
            //}
            //if (!oldCode.ToString().Equals(order.Code))
            //{
            //    return Json(new MessageResult { Status = false, Message = "验证码错误" });
            //}
            
            var orderId = order.Id;
            if (order.Id == Guid.Empty)
            {
                order.Code = string.Empty;
                order.CreatedBy = order.ModifiedBy = this.User.Id;
                orderId = this._IF_OrderService.Create(order).Id;
            }
            else
            {
                var oldOrder = this._IF_OrderService.GetByKey(order.Id);
                oldOrder.ModifiedBy = this.User.Id;
                oldOrder.Name = order.Name;
                oldOrder.IDNo = order.IDNo;
                order.PersonalPhone = order.PersonalPhone;

                this._IF_OrderService.Update(new List<F_OrderDTO> { oldOrder });
            }

            return Json(new MessageResult { Status = true, Message = "个人信息保存成功", Data = orderId });
        }

        /// <summary>
        /// 02：填写房产信息
        /// </summary>
        /// <param name="order">订单参数</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Step02(F_OrderDTO order)
        {
            if (order == null
                || string.IsNullOrWhiteSpace(order.Community)
                || string.IsNullOrWhiteSpace(order.Landlord))
            {
                return Json(new MessageResult { Status = false, Message = "资料不全" });
            }

            if (order.IsInstallment)
            {
                if (string.IsNullOrWhiteSpace(order.FromBank))
                {
                    return Json(new MessageResult { Status = false, Message = "资料不全" });
                }
            }

            var model = this._IF_OrderService.GetByKey(order.Id);
            if (model == null)
            {
                return Json(new MessageResult { Status = false, Message = "无效订单" });
            }
            model.Province = order.Province;
            model.City = order.City;
            model.Country = order.Country;

            model.Community = order.Community;
            model.Landlord = order.Landlord;
            model.IsInstallment = order.IsInstallment;
            model.FromBank = order.FromBank;
            model.ModifiedBy = this.User.Id;
            this._IF_OrderService.Update(new List<F_OrderDTO> { model });

            return Json(new MessageResult { Status = true, Message = "房产信息保存成功" });
        }

        /// <summary>
        /// 03：填写交易信息
        /// </summary>
        /// <param name="order">订单参数</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Step03(F_OrderDTO order)
        {
            if (string.IsNullOrWhiteSpace(order.StoreCode)
                    //|| string.IsNullOrWhiteSpace(order.ClerkCode)
                )
            {
                return Json(new MessageResult { Status = false, Message = "资料不全" });
            }
            bool submit = order.IsActive;
            var model = this._IF_OrderService.GetByKey(order.Id);
            if (model == null)
            {
                return Json(new MessageResult { Status = false, Message = "无效订单" });
            }

            var store = this._IF_StorerService.GetStoreByCode(order.StoreCode);

            if (store == null)
            {
                return Json(new MessageResult { Status = false, Message = "店铺无效" });
            }
            //如果导购工号未填写，则不需要校验导购和商铺的从属关系
            //if(!string.IsNullOrWhiteSpace(order.ClerkCode))
            //{
            //    var boundStore = this._IF_StorerService.GetStoreByClerkId(order.ClerkCode);
            //    if (boundStore == null || boundStore.Code != order.StoreCode)
            //    {
            //        return Json(new MessageResult { Status = false, Message = "导购和店铺不匹配" });
            //    }
            //}
            
            model.StoreCode = order.StoreCode;
            model.ClerkCode = order.ClerkCode;

            model.TotalAmount = order.TotalAmount;
            model.LoanAmount = order.LoanAmount;
            model.DownpaymentAmount = order.DownpaymentAmount;

            model.ModifiedBy = this.User.Id;
            this._IF_OrderService.Update(new List<F_OrderDTO> { model });

            if (submit)
            {
                this.Submit(model.Id);
                return Json(new MessageResult { Status = true, Message = "订单提交成功" });
            }
            return Json(new MessageResult { Status = true, Message = "订单保存成功" });
        }


        /// <summary>
        /// 04：填写征信信息
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Step04(Guid id, bool submit = true)
        {
            var files = this._IF_FileService.GetFilesByReferenceId(id).Where(item => item.Code.Equals("4"));
            if (files.Count() == 0)
            {
                return Json(new MessageResult { Status = false, Message = "请上传征信资料" });
            }
            if (submit)
                return this.Submit(id);
            return Json(new MessageResult { Status = true, Message = "订单保存成功" });
        }

        /// <summary>
        /// 提交订单
        /// </summary>
        /// <param name="id">订单号</param>
        /// <returns></returns>
        private IHttpActionResult Submit(Guid id)
        {
            var order = this._IF_OrderService.GetByKey(id);
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "无效订单" });
            }
            var store = this._IF_StorerService.GetStoreByCode(order.StoreCode);
            //1、分配订单到Gojiaju金融客服
            string gojiajuClerkCode = this._IF_OrderService.AssignOrderClerk(store.WebsiteId);
            if (string.IsNullOrWhiteSpace(gojiajuClerkCode))
            {
                return Json(new MessageResult { Status = false, Message = "提交失败，当前站点无金融客服" });
            }
            order.GoJiajuClerkCode = gojiajuClerkCode;
            order.ModifiedBy = this.User.Id;
            order.Status = F_OrderStatusEnum.InProcess;
            this._IF_OrderService.Update(new List<F_OrderDTO> { order });

            this.CreateMessage(order, new F_OrderRecordDTO());
            Ingenious.Infrastructure.Message.MessageService.SMSSend("", "");

            return Json(new MessageResult { Status = true, Message = "订单提交成功" });
        }

        /// <summary>
        /// 订单审核
        /// </summary>
        /// <param name="id">订单标识（F_Order.Id）</param>
        /// <param name="status">订单状态：F_OrderStatusEnum</param>
        /// <param name="remark">审核备注</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Process(Guid id, F_OrderStatusEnum status, string remark = "")
        {
            if (this.User.UserType == F_UserTypeEnum.BC
                || this.User.UserType == F_UserTypeEnum.BM)
            {
                var order = this._IF_OrderService.GetByKey(id);
                if (order == null)
                {
                    return Json(new MessageResult { Status = false, Message = "无效订单" });
                }
                order.Status = status;
                order.ModifiedBy = this.User.Id;
                this._IF_OrderService.Update(new List<F_OrderDTO> { order });
                var orderRecord = new F_OrderRecordDTO
                {
                    CreatedBy = this.User.Id,
                    ModifiedBy = this.User.Id,
                    Status = status,
                    OrderId = id,
                    Remark = remark,
                    UserId = this.User.Id,
                };
                this._IF_OrderRecordService.Create(orderRecord);
                return Json(new MessageResult { Status = true, Message = "处理成功" });
            }

            return Json(new MessageResult { Status = false, Message = "对该请求的授权已被拒绝" });
        }


        /// <summary>
        /// 获取订单
        /// </summary>
        /// <param name="clerkCode">店员编号</param>
        /// <param name="storeCode">店铺编号</param>
        /// <param name="brankId">银行编号</param>
        /// <param name="userId">关联用户</param>
        /// <param name="status">订单状态</param>
        /// <param name="min">申请金额范围：最小值</param>
        /// <param name="max">申请金额范围：最大值</param>
        /// <returns></returns>
        public IHttpActionResult GetOrders(F_OrderStatusEnum? status = null,
            string clerkCode = "",
            string storeCode = "",
            string fromBank = "",
            Guid? userId = null,
            string bankManager = "",
            string bankClerkCode = "",
            string gojiajuClerkCode = "",
                        string keyword = "",
            string date = "",
            decimal? min = null,
            decimal? max = null)
        {
            var orders = new F_OrderDTOListWithPagination();

            orders = this._IF_OrderService.GetAll(1, int.MaxValue, clerkCode, storeCode, fromBank, userId, status, bankManager, bankClerkCode, gojiajuClerkCode, keyword, date, min, max);

            return Json(orders.Rows);
        }


        #region 银行

        /// <summary>
        /// 根据银行编号获取订单数量
        /// 返回JSON数据结构
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </summary>
        /// <returns>
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetOrderCountByBank()
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);

            if (this.User.UserType != F_UserTypeEnum.BM
                || string.IsNullOrWhiteSpace(user.BankCode))
            {
                return Json(new MessageResult { Status = false, Message = "所属银行未设置" });
            }

            var result = new { InProcess = 0, Presigned = 0, Prepassed = 0, Passed = 0, Failed = 0 };

            var list = new F_OrderDTOListWithPagination();

            list = this._IF_OrderService.GetAll(1, int.MaxValue, null, null, user.BankCode);

            if (list.Rows.Count() == 0)
            {
                return Json(new MessageResult { Status = true, Data = result });
            }
            result = new
            {
                InProcess = list.Rows.Where(item => item.Status == F_OrderStatusEnum.GojiajuPassed).Count(),

                Presigned = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankPassed).Count(),
                Prepassed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankSigned).Count(),
                Passed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.Successed).Count(),
                Failed = list.Rows.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                                    || (item.Status == F_OrderStatusEnum.SignCanceled)).Count()
            };

            return Json(new MessageResult { Status = true, Data = result });
        }
        /// <summary>
        /// 根据银行编号获取订单
        /// 返回JSON数据结构
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </summary>
        /// <param name="status">订单阶段：
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </param>
        /// <returns>
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </returns>

        [HttpGet]
        public IHttpActionResult GetOrderByBank(string status = "", string keyword = "", string date = "", string bankClerk = "")
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);

            if (this.User.UserType != F_UserTypeEnum.BM
                || string.IsNullOrWhiteSpace(user.BankCode))
            {
                return Json(new MessageResult { Status = false, Message = "所属银行未设置" });
            }

            var list = new F_OrderDTOListWithPagination();

            list = this._IF_OrderService.GetAll(1, int.MaxValue, null, null, user.BankCode, null, null, null, bankClerk, null, keyword, date);

            // 默认返回所有状态订单
            //if (!string.IsNullOrWhiteSpace(status))
            {
                switch ((status ?? "").ToLower())
                {
                    case "inprocess":
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.GojiajuPassed) });
                        }
                    case "presigned":
                        {
                            return Json(new MessageResult
                            {
                                Status = true,
                                Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankPassed)
                            });
                        }
                    case "prepassed":
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankSigned) });
                        }
                    case "passed":
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.Successed) });
                        }
                    case "failed":
                        {
                            return Json(new MessageResult
                            {
                                Status = true,
                                Data = list.Rows.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                                    || (item.Status == F_OrderStatusEnum.SignCanceled))
                            });
                        }
                    default:
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows });
                        }
                }
            }

            if (list.Rows.Count() == 0)
            {
                var defaultResult = new { InProcess = new List<F_OrderDTO>(), Presigned = new List<F_OrderDTO>(), Prepassed = new List<F_OrderDTO>(), Passed = new List<F_OrderDTO>(), Failed = new List<F_OrderDTO>() };

                return Json(new MessageResult { Status = true, Data = defaultResult });
            }
            var result = new
            {
                InProcess = list.Rows.Where(item => item.Status == F_OrderStatusEnum.GojiajuPassed),

                Presigned = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankPassed),
                Prepassed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankSigned),
                Passed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.Successed),
                Failed = list.Rows.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                || (item.Status == F_OrderStatusEnum.SignCanceled)
                )
            };

            return Json(new MessageResult { Status = true, Data = result });
        }

        #endregion

        #region 银行客户经理

        /// <summary>
        /// 根据客户经理编号获取订单数量
        /// 返回JSON数据结构
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </summary>
        /// <returns>
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetOrderCountByBankClerk()
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);

            if (user == null || string.IsNullOrWhiteSpace(user.Code))
            {
                return Json(new MessageResult { Status = false, Message = GlobalMessage.Unauthorized });
            }
            var result = new { InProcess = 0, Presigned = 0, Prepassed = 0, Passed = 0, Failed = 0 };
            var list = new F_OrderDTOListWithPagination();

            list = this._IF_OrderService.GetAll(1, int.MaxValue, null, null, null, null, null, null, user.Code);

            if (list.Rows.Count() == 0)
            {
                return Json(new MessageResult { Status = true, Data = result });
            }

            result = new
            {
                InProcess = list.Rows.Where(item => item.Status == F_OrderStatusEnum.GojiajuPassed).Count(),

                Presigned = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankPassed).Count(),
                Prepassed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankSigned).Count(),
                Passed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.Successed).Count(),
                Failed = list.Rows.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                            || (item.Status == F_OrderStatusEnum.SignCanceled)).Count()
            };

            return Json(new MessageResult { Status = true, Data = result });
        }

        /// <summary>
        /// 根据客户经理编号获取订单
        /// 返回JSON数据结构
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </summary>
        /// <param name="status">订单阶段：
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </param>
        /// <returns>
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetOrderByBankClerk(string status = "", string keyword = "", string date = "")
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);

            if (user == null || string.IsNullOrWhiteSpace(user.Code))
            {
                return Json(new MessageResult { Status = false, Message = GlobalMessage.Unauthorized });
            }
            var list = new F_OrderDTOListWithPagination();

            list = this._IF_OrderService.GetAll(1, int.MaxValue, null, null, null, null, null, null, user.Code, null, keyword, date);

            // 默认返回所有状态订单
            //if (!string.IsNullOrWhiteSpace(status))
            {
                switch ((status ?? "").ToLower())
                {
                    case "inprocess":
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.GojiajuPassed) });
                        }
                    case "presigned":
                        {
                            return Json(new MessageResult
                            {
                                Status = true,
                                Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankPassed)
                            });
                        }
                    case "prepassed":
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankSigned) });
                        }
                    case "passed":
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.Successed) });
                        }
                    case "failed":
                        {
                            return Json(new MessageResult
                            {
                                Status = true,
                                Data = list.Rows.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                                    || (item.Status == F_OrderStatusEnum.SignCanceled))
                            });
                        }
                    default:
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows });
                        }
                }
            }

            if (list.Count() == 0)
            {
                var defultResult = new { InProcess = 0, Presigned = 0, Prepassed = 0, Passed = 0, Failed = 0 };
                return Json(new MessageResult { Status = true, Data = defultResult });
            }

            var result = new
            {
                InProcess = list.Rows.Where(item => item.Status == F_OrderStatusEnum.GojiajuPassed),

                Presigned = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankPassed),
                Prepassed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankSigned),
                Passed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.Successed),
                Failed = list.Rows.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                || (item.Status == F_OrderStatusEnum.SignCanceled)
                )
            };
            return Json(new MessageResult { Status = true, Data = result });
        }

        #endregion

        #region Go佳居客服

        /// <summary>
        /// 根据Go佳居客服编号获取订单数量
        /// 返回JSON数据结构
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </summary>
        /// <returns>
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetOrderCountByGojiajuClerk()
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);

            if (user == null || string.IsNullOrWhiteSpace(user.Code))
            {
                return Json(new MessageResult { Status = false, Message = GlobalMessage.Unauthorized });
            }
            var result = new { InProcess = 0, Presigned = 0, Prepassed = 0, Passed = 0, Failed = 0 };
            var list = new F_OrderDTOListWithPagination();

            list = this._IF_OrderService.GetAll(1, int.MaxValue, null, null, null, null, null, null, null, user.Code);

            if (list.Count() == 0)
            {
                return Json(new MessageResult { Status = true, Data = result });
            }

            result = new
            {
                InProcess = list.Rows.Where(item => item.Status == F_OrderStatusEnum.InProcess).Count(),

                Presigned = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankPassed).Count(),
                Prepassed = list.Where(item => item.Status == F_OrderStatusEnum.BankSigned).Count(),
                Passed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.Successed).Count(),
                Failed = list.Rows.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                            || (item.Status == F_OrderStatusEnum.SignCanceled)).Count()
            };

            return Json(new MessageResult { Status = true, Data = result });
        }

        /// <summary>
        /// 根据Go佳居客服编号获取订单
        /// 返回JSON数据结构
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </summary>
        /// <param name="status">订单阶段：
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </param>
        /// <returns>
        /// {
        ///     InProcess：待受理，
        ///     Presigned：待签约，
        ///     Prepassed：待放款，
        ///     Passed：已放款，
        ///     Failed：已失效
        /// }
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetOrderByGojiajuClerk(string status = "", string keyword = "", string date = "")
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);

            if (user == null || string.IsNullOrWhiteSpace(user.Code))
            {
                return Json(new MessageResult { Status = false, Message = GlobalMessage.Unauthorized });
            }
            var list = new F_OrderDTOListWithPagination();

            list = this._IF_OrderService.GetAll(1, int.MaxValue, null, null, null, null, null, null, null, user.Code, keyword, date);

            //if (!string.IsNullOrWhiteSpace(status))
            {
                switch ((status ?? "").ToLower())
                {
                    case "inprocess":
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.InProcess) });
                        }
                    case "presigned":
                        {
                            return Json(new MessageResult
                            {
                                Status = true,
                                Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankPassed)
                            });
                        }
                    case "prepassed":
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankSigned) });
                        }
                    case "passed":
                        {
                            return Json(new MessageResult { Status = true, Data = list.Rows.Where(item => item.Status == F_OrderStatusEnum.Successed) });
                        }
                    case "failed":
                        {
                            return Json(new MessageResult
                            {
                                Status = true,
                                Data = list.Rows.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                                    || (item.Status == F_OrderStatusEnum.SignCanceled))
                            });
                        }
                    default:
                        {
                            return Json(new MessageResult { Status = true, Data = list });
                        }
                }
            }

            if (list.Count() == 0)
            {
                var defultResult = new { InProcess = 0, Presigned = 0, Prepassed = 0, Passed = 0, Failed = 0 };
                return Json(new MessageResult { Status = true, Data = defultResult });
            }

            var result = new
            {
                InProcess = list.Rows.Where(item => item.Status == F_OrderStatusEnum.InProcess),

                Presigned = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankPassed),
                Prepassed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.BankSigned),
                Passed = list.Rows.Where(item => item.Status == F_OrderStatusEnum.Successed),
                Failed = list.Rows.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                || (item.Status == F_OrderStatusEnum.SignCanceled)
                )
            };
            return Json(new MessageResult { Status = true, Data = result });
        }

        #endregion

        #region 消费者
        /// <summary>
        /// 根据用户Id获取订单数量
        /// 返回JSON数据结构
        /// {
        ///     Status:true
        ///     Data:{
        ///             InProcess：正在申请的数量，
        ///             Passed：已通过的数量，
        ///             Failed：已拒绝的数量
        ///         }
        /// }
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult GetOrderCountByUserId()
        {
            var list = this._IF_OrderService.GetAll(1, int.MaxValue, null, null, null, this.User.Id);
            //var list = this._IF_OrderService.GetAll(null, null, null, new Guid("6f7ed9a1-f762-e711-8127-0019b93d4f5e"));
            var result = new { Temp = 0, InProcess = 0, Passed = 0, Failed = 0 };
            if (list.Count() == 0)
            {
                return Json(new MessageResult { Status = true, Data = result });
            }

            result = new
             {
                 Temp = list.Where(item => item.Status == F_OrderStatusEnum.Temp).Count(),
                 InProcess = list.Where(item => (item.Status != F_OrderStatusEnum.SignCanceled)
                     && (item.Status != F_OrderStatusEnum.Canceled)
                     && (item.Status != F_OrderStatusEnum.Successed)
                     && (item.Status != F_OrderStatusEnum.Temp)).Count(),
                 Passed = list.Where(item => item.Status == F_OrderStatusEnum.Successed).Count(),
                 Failed = list.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                 || (item.Status == F_OrderStatusEnum.SignCanceled)
                 ).Count()
             };

            return Json(new MessageResult { Status = true, Data = result });
        }

        /// <summary>
        /// 根据用户Id获取订单
        /// 返回JSON数据结构
        /// {
        ///     Status:true
        ///     Data:{
        ///             InProcess：正在申请的订单，
        ///             Passed：已通过的订单，
        ///             Failed：已拒绝的订单
        ///         }
        /// }
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IHttpActionResult GetOrderByUserId(string keyword = "")
        {
            var list = this._IF_OrderService.GetAll(1, int.MaxValue, null, null, null, this.User.Id, null, null, null, null, keyword);

            if (list.Count() == 0)
            {
                var defaultResult = new { Temp = new List<F_OrderDTO>(), InProcess = new List<F_OrderDTO>(), Passed = new List<F_OrderDTO>(), Failed = new List<F_OrderDTO>(), All = new List<F_OrderDTO>() };
                return Json(new MessageResult { Status = true,Message="0", Data = defaultResult });
            }

            var result = new
             {
                 Temp = list.Where(item => item.Status == F_OrderStatusEnum.Temp),
                 InProcess = list.Where(item => (item.Status != F_OrderStatusEnum.SignCanceled)
                     && (item.Status != F_OrderStatusEnum.Canceled)
                     && (item.Status != F_OrderStatusEnum.Successed)
                     && (item.Status != F_OrderStatusEnum.Temp)),
                 Passed = list.Where(item => item.Status == F_OrderStatusEnum.Successed),
                 Failed = list.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                 || (item.Status == F_OrderStatusEnum.SignCanceled)),
                 All = list
             };

            return Json(new MessageResult { Status = true, Data = result });
        }

        #endregion

        /// <summary>
        /// 获取导购收益记录
        /// 返回JSON数据结构
        /// {
        ///     Data: F_OrderDTO集合
        /// }
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// JSON数据结构
        /// {
        ///     Name = yyyy年MM月,
        ///     List = [{Id:,Code="GFQ20170724001",Money=100,DateTime="7.24", DayOfWeek="星期一" }]
        /// }
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetOrderByClerkId(Guid userId)
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(userId);
            var list = this._IF_OrderService.GetAll(1, int.MaxValue, user.Code, null, null, null, F_OrderStatusEnum.Successed, null, null);

            var result = new List<dynamic>();
            var groupedList = list.GroupBy(item => item.CreatedDate.ToString("yyyy年M月"));
            foreach (IGrouping<string, F_OrderDTO> item in groupedList)
            {
                result.Add(new
                {
                    Name = item.First().CreatedDate.ToString("yyyy年M月"),
                    List = this.GetFormatOrder(item.ToList())
                });
            }

            return Json(new MessageResult { Status = true, Message = "", Data = result.OrderByDescending(item => item.Name) });
        }

        private List<dynamic> GetFormatOrder(List<F_OrderDTO> list)
        {
            var result = new List<dynamic>();
            foreach (var item in list)
            {
                result.Add(new
                {
                    Id = item.Id,
                    Code = item.Code,
                    Money = (item.LoanAmount * 0.01m),
                    DateTime = item.CreatedDate.ToString("M.dd"),
                    DayOfWeek = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(item.CreatedDate.DayOfWeek)
                });
            }
            return result;
        }

        /// <summary>
        /// 获取导购交易记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetWithdrawDepositRecord()
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);
            var list = _IF_WithdrawDepositRecordService.GetAll(user.Code);

            var result = new List<dynamic>();
            var groupedList = list.GroupBy(item => item.CreatedDate.ToString("yyyy年M月"));
            foreach (IGrouping<string, F_WithdrawDepositRecordDTO> item in groupedList)
            {
                result.Add(new
                {
                    Name = item.First().CreatedDate.ToString("yyyy年M月"),
                    List = this.GetFormatWithdrawDepositRecord(item.ToList())
                });
            }

            return Json(new MessageResult { Status = true, Data = result });
        }

        private List<dynamic> GetFormatWithdrawDepositRecord(List<F_WithdrawDepositRecordDTO> list)
        {
            var result = new List<dynamic>();
            foreach (var item in list)
            {
                result.Add(new
                {
                    Id = item.Id,
                    Code = item.TradeNo,
                    Money = item.Money,
                    DateTime = item.CreatedDate.ToString("M.dd"),
                    DayOfWeek = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(item.CreatedDate.DayOfWeek)
                });
            }
            return result;
        }

        /// <summary>
        /// 获取导购分期记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetClerkOrders(string status = "", string keyword = "", string date = "")
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);
            var list = this._IF_OrderService.GetAll(1, int.MaxValue, user.Code, null, null, null, null, null, null, null, keyword, date)
            .Where(item => item.Status != F_OrderStatusEnum.Temp
                && item.Status != F_OrderStatusEnum.Canceled)
                .OrderByDescending(item => item.CreatedDate);
            switch ((status ?? "").ToLower())
            {
                case "inprocess":
                    {
                        return Json(new MessageResult { Status = true, Data = list.Where(item => item.Status == F_OrderStatusEnum.InProcess) });
                    }
                case "presigned":
                    {
                        return Json(new MessageResult
                        {
                            Status = true,
                            Data = list.Where(item => item.Status == F_OrderStatusEnum.BankPassed)
                        });
                    }
                case "prepassed":
                    {
                        return Json(new MessageResult { Status = true, Data = list.Where(item => item.Status == F_OrderStatusEnum.BankSigned) });
                    }
                case "passed":
                    {
                        return Json(new MessageResult { Status = true, Data = list.Where(item => item.Status == F_OrderStatusEnum.Successed) });
                    }
                case "failed":
                    {
                        return Json(new MessageResult
                        {
                            Status = true,
                            Data = list.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                                || (item.Status == F_OrderStatusEnum.SignCanceled))
                        });
                    }
                default:
                    {
                        return Json(new MessageResult { Status = true, Data = list });
                    }
            }
        }

        /// <summary>
        /// 店铺模块：获取导购分期记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStoreOrders(string status = "", string keyword = "", string date = "")
        {
            var store = this._IF_StorerService.GetStoreByEnglishName(this.User.UserName);
            var list = this._IF_OrderService.GetAll(1, int.MaxValue, null, store.Code, null, null, null, null, null, null, keyword, date)
                .Where(item => item.Status != F_OrderStatusEnum.Temp
                && item.Status != F_OrderStatusEnum.Canceled)
                .OrderByDescending(item => item.CreatedDate);
            switch ((status ?? "").ToLower())
            {
                case "inprocess":
                    {
                        return Json(new MessageResult { Status = true, Data = list.Where(item => item.Status == F_OrderStatusEnum.InProcess) });
                    }
                case "presigned":
                    {
                        return Json(new MessageResult
                        {
                            Status = true,
                            Data = list.Where(item => item.Status == F_OrderStatusEnum.BankPassed)
                        });
                    }
                case "prepassed":
                    {
                        return Json(new MessageResult { Status = true, Data = list.Where(item => item.Status == F_OrderStatusEnum.BankSigned) });
                    }
                case "passed":
                    {
                        return Json(new MessageResult { Status = true, Data = list.Where(item => item.Status == F_OrderStatusEnum.Successed) });
                    }
                case "failed":
                    {
                        return Json(new MessageResult
                        {
                            Status = true,
                            Data = list.Where(item => (item.Status == F_OrderStatusEnum.Canceled)
                                || (item.Status == F_OrderStatusEnum.SignCanceled))
                        });
                    }
                default:
                    {
                        return Json(new MessageResult { Status = true, Data = list });
                    }
            }
        }

        /// <summary>
        /// 更新订单状态
        /// 1、平台更新（审核）
        /// 2、银行审核（审核）
        /// 3、银行更新（放款）
        /// </summary>
        /// <param name="id">订单Id</param>
        /// <param name="status">订单状态（数字）F_OrderStatusEnum</param>
        /// <param name="remark">备注信息</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult UpdateStatus(Guid id, F_OrderStatusEnum status, string remark = "")
        {
            var order = this._IF_OrderService.GetByKey(id);
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "订单无效" });
            }
            order.Status = status;
            this._IF_OrderService.Update(new List<F_OrderDTO> { order });

            //保存状态变更记录
            var orderRecord = new F_OrderRecordDTO();
            orderRecord.Status = status;
            orderRecord.CreatedBy = orderRecord.ModifiedBy = this.User.Id;
            orderRecord.OrderId = id;
            orderRecord.Remark = remark;
            orderRecord.Status = status;
            orderRecord.UserId = this.User.Id;
            this._IF_OrderRecordService.Create(orderRecord);

            return Json(new MessageResult { Status = true, Message = "处理成功" });
        }

        /// <summary>
        /// 修改订单
        /// </summary>
        /// <param name="order">订单参数</param>
        /// <returns></returns>
        public IHttpActionResult UpdateOrder(F_OrderDTO order)
        {
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "订单无效" });
            }

            var model = this._IF_OrderService.GetByKey(order.Id);

            if (model == null)
            {
                return Json(new MessageResult { Status = false, Message = "订单无效" });
            }

            model.Name = order.Name;
            model.IDNo = order.IDNo;

            model.Community = order.Community;
            model.Province = order.Province;
            model.City = order.City;
            model.Country = order.Country;
            model.Landlord = order.Landlord;
            model.IsInstallment = order.IsInstallment;
            model.FromBank = order.FromBank;

            model.TotalAmount = order.TotalAmount;
            model.DownpaymentAmount = order.DownpaymentAmount;
            model.LoanAmount = order.LoanAmount;

            this._IF_OrderService.Update(new List<F_OrderDTO> { model });

            return Json(new MessageResult { Status = true, Message = "订单修改成功" });
        }

        /// <summary>
        /// 创建订单审核记录
        /// </summary>
        /// <param name="orderRecord">审核记录对象</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateOrderRecord(F_OrderRecordDTO orderRecord)
        {
            if (orderRecord == null || orderRecord.OrderId == Guid.Empty)
            {
                return Json(new MessageResult { Status = false, Message = "订单无效" });
            }

            orderRecord.UserId = this.User.Id;
            orderRecord.CreatedBy = orderRecord.ModifiedBy = this.User.Id;

            var order = this._IF_OrderService.GetByKey(orderRecord.OrderId);
            order.Status = orderRecord.Status;
            //switch (orderRecord.Status)
            //{
            //    case F_OrderStatusEnum.GojiajuDenied:
            //    case F_OrderStatusEnum.BankDenied:
            //        {
            //            order.BankCode = string.Empty;
            //            order.GoJiajuClerkCode = string.Empty;
            //            order.BankManager = string.Empty;
            //            order.BankClerk = string.Empty;
            //        }
            //        break;
            //}

            if (orderRecord.Status == F_OrderStatusEnum.GojiajuPassed)
            {
                if (string.IsNullOrWhiteSpace(orderRecord.BankCode))
                {
                    return Json(new MessageResult { Status = false, Message = "未分配银行" });
                }
                //1、分配银行
                order.BankCode = orderRecord.BankCode;

                var bank = this._IF_BankService.GetBank(orderRecord.BankCode);
                if (bank == null)
                {
                    return Json(new MessageResult { Status = false, Message = "未分配银行" });
                }
                //2、分配订单到银行客户经理
                if (bank.IsAssignAuto)
                {
                    var clerkCode = this._IF_BankService.AssignOrderToClerk(bank.Code);
                    if (string.IsNullOrWhiteSpace(clerkCode))
                    {
                        return Json(new MessageResult { Status = false, Message = "分配订单失败：银行未设置客户经理或账号不可用" });
                    }
                    order.BankClerk = clerkCode;
                }

                //3、分配订单到银行信贷经理
                var bankManager = this._IF_UserDetailService.GetUserDetailByBankCode(orderRecord.BankCode).Where(item => item.F_User.UserType == F_UserTypeEnum.BM && item.F_User.IsActive).FirstOrDefault();
                if (bankManager == null)
                {
                    return Json(new MessageResult { Status = false, Message = "分配订单失败：银行未设置信贷经理或账号不可用" });
                }
                order.BankManager = bankManager.Code;
            }

            //实际发放金额
            order.GotLoanAmount = orderRecord.GotLoanMoney;

            this._IF_OrderService.Update(new List<F_OrderDTO> { order });
            this._IF_OrderRecordService.Create(orderRecord);

            this.CreateMessage(order, orderRecord);

            if (orderRecord.Status == F_OrderStatusEnum.GojiajuPassed)
            {
                return Json(new MessageResult { Status = true, Message = "订单已分配" });
            }

            return Json(new MessageResult { Status = true, Message = "保存成功" });
        }

        /// <summary>
        /// 分配订单
        /// </summary>
        /// <param name="orderId">订单Id（F_Order.Id）</param>
        /// <param name="clerkCode">银行客户经理Code(F_User.Code)</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult AssignOrder(Guid orderId, string clerkCode)
        {
            if (string.IsNullOrWhiteSpace(clerkCode))
            {
                return Json(new MessageResult { Status = false, Message = "未分配客服经理" });
            }
            if (this.User.UserType != F_UserTypeEnum.BM)
            {
                return Json(new MessageResult { Status = false, Message = GlobalMessage.Unauthorized });
            }
            var order = this._IF_OrderService.GetByKey(orderId);
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "订单无效" });
            }

            order.BankClerk = clerkCode;
            this._IF_OrderService.Update(new List<F_OrderDTO> { order });

            return Json(new MessageResult { Status = true, Message = "订单分配成功" });
        }

        /// <summary>
        /// 编辑订单
        /// 1、消费者在平台未受理时可修改订单
        /// 2、平台拒绝、银行拒绝时可修改订单
        /// </summary>
        /// <param name="order">订单参数</param>
        /// <returns></returns>
        public IHttpActionResult ResetOrder(F_OrderDTO order)
        {
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "订单无效" });
            }

            var model = this._IF_OrderService.GetByKey(order.Id);

            if (model == null)
            {
                return Json(new MessageResult { Status = false, Message = "订单无效" });
            }

            if (string.IsNullOrWhiteSpace(order.Name))
            {
                return Json(new MessageResult { Status = false, Message = "姓名不能为空" });
            }

            if (string.IsNullOrWhiteSpace(order.IDNo))
            {
                return Json(new MessageResult { Status = false, Message = "身份账号不能为空" });
            }

            if (string.IsNullOrWhiteSpace(order.Community))
            {
                return Json(new MessageResult { Status = false, Message = "房产所在小区不能为空" });
            }
            if (string.IsNullOrWhiteSpace(order.Landlord))
            {
                return Json(new MessageResult { Status = false, Message = "户主姓名不能为空" });
            }

            if (order.IsInstallment && string.IsNullOrWhiteSpace(order.FromBank))
            {
                return Json(new MessageResult { Status = false, Message = "贷款银行不能为空" });
            }
            else
            {
                order.FromBank = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(order.StoreCode))
            {
                return Json(new MessageResult { Status = false, Message = "所属商户不能为空" });
            }
            if (string.IsNullOrWhiteSpace(order.ClerkCode))
            {
                return Json(new MessageResult { Status = false, Message = "导购工号不能为空" });
            }

            if (order.TotalAmount == 0)
            {
                return Json(new MessageResult { Status = false, Message = "交易总额必须大于零" });
            }

            if (order.DownpaymentAmount == 0)
            {
                return Json(new MessageResult { Status = false, Message = "首付金额必须大于零" });
            }

            if (order.LoanAmount == 0)
            {
                return Json(new MessageResult { Status = false, Message = "贷款金额必须大于零" });
            }

            if (!order.IsInstallment)
            {
                order.FromBank = string.Empty;
            }
            model.Name = order.Name;
            model.IDNo = order.IDNo;

            model.Community = order.Community;
            model.Province = order.Province;
            model.City = order.City;
            model.Country = order.Country;
            model.Landlord = order.Landlord;
            model.IsInstallment = order.IsInstallment;
            model.FromBank = order.FromBank;

            model.TotalAmount = order.TotalAmount;
            model.DownpaymentAmount = order.DownpaymentAmount;
            model.LoanAmount = order.LoanAmount;

            this._IF_OrderService.Update(new List<F_OrderDTO> { model }).FirstOrDefault();
            return Submit(order.Id);

            ////如果是未提交订单，则按照分配原则提交。
            //if (order.Status == F_OrderStatusEnum.Temp)
            //{
            //    this._IF_OrderService.Update(new List<F_OrderDTO> { model }).FirstOrDefault();
            //    return Submit(order.Id);
            //}//如果是平台拒绝、银行拒绝则将状态修改为提交状态。
            //else
            //{
            //    order.Status = F_OrderStatusEnum.InProcess;
            //    order = this._IF_OrderService.Update(new List<F_OrderDTO> { model }).FirstOrDefault();
            //}

            //return Json(new MessageResult { Status = true, Message = "订单提交成功" });
        }

        private void CreateMessage(F_OrderDTO order, F_OrderRecordDTO record)
        {
            var message = new F_ActivityDTO();

            switch (order.Status)
            {
                case F_OrderStatusEnum.InProcess:
                    {
                        message.Title = "您有一笔新订单提交成功！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：待审核",order.Id, order.Code);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = this.User.Id;
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        var user = this._IF_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔新订单待处理！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：待审核", order.Id, order.Code);
                            message.ReferenceId = user.F_UserId;
                            this._IF_ActivityService.Create(message);
                        }
                    }
                    break;
                case F_OrderStatusEnum.GojiajuDenied:
                    {
                        message.Title = "您有一笔订单未通过平台审核！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        message.Title = "您拒绝了一笔订单申请！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台拒绝", order.Id, order.Code);
                        message.ReferenceId = this.User.Id;
                        this._IF_ActivityService.Create(message);
                    }
                    break;
                case F_OrderStatusEnum.GojiajuPassed:
                    {
                        message.Title = "您有一笔订单通过平台审核！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台通过<br/>", order.Id, order.Code);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        message.Title = "您通过了一笔订单申请！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台通过", order.Id, order.Code);
                        message.ReferenceId = this.User.Id;//平台
                        this._IF_ActivityService.Create(message);

                        var bankManager = this._IF_UserDetailService.GetUserDetailByCode(order.BankManager);
                        if (bankManager != null)
                        {
                            message.Title = "您有一笔订单待处理！";//银行信贷经理
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台通过", order.Id, order.Code);
                            message.ReferenceId = bankManager.F_UserId;
                            this._IF_ActivityService.Create(message);
                        }

                        var bankClient = this._IF_UserDetailService.GetUserDetailByCode(order.BankManager);
                        if (bankClient != null)
                        {
                            message.Title = "您有一笔订单待处理！";//银行客户经理
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：平台通过", order.Id, order.Code);
                            message.ReferenceId = bankManager.F_UserId;
                            this._IF_ActivityService.Create(message);
                        }
                    }
                    break;
                case F_OrderStatusEnum.BankDenied:
                    {
                        message.Title = "您有一笔订单未通过银行审核！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        message.Title = "您拒绝了一笔订单申请！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.ReferenceId = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        var user = this._IF_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单未通过银行审核！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                            message.ReferenceId = user.F_UserId;
                            this._IF_ActivityService.Create(message);
                        }
                    }
                    break;
                case F_OrderStatusEnum.BankPassed:
                    {
                        message.Title = "您有一笔订单通过银行审核！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行通过", order.Id, order.Code);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        message.Title = "您通过了一笔订单申请！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行通过", order.Id, order.Code);
                        message.ReferenceId = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        var user = this._IF_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单通过银行审核！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行通过", order.Id, order.Code);
                            message.ReferenceId = user.F_UserId;
                            this._IF_ActivityService.Create(message);
                        }
                    }
                    break;
                case F_OrderStatusEnum.BankSigned:
                    {
                        message.Title = "您有一笔订单签约成功！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        message.Title = "您签约了一笔订单申请！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.ReferenceId = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        var user = this._IF_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单签约成功！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                            message.ReferenceId = user.F_UserId;
                            this._IF_ActivityService.Create(message);
                        }
                    }
                    break;
                case F_OrderStatusEnum.SignCanceled:
                    {
                        message.Title = "您有一笔订单签约取消！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        message.Title = "您取消了一笔订单签约！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                        message.ReferenceId = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        var user = this._IF_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单签约取消！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：银行拒绝<br/>备注：{2}", order.Id, order.Code, record.Remark);
                            message.ReferenceId = user.F_UserId;
                            this._IF_ActivityService.Create(message);
                        }
                    }
                    break;
                case F_OrderStatusEnum.Successed:
                    {
                        message.Title = "您有一笔订单放款成功！";
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：已放款<br/>申请金额：{2}<br/>实际放款：{3}万", order.Id, order.Code, order.LoanAmount, order.GotLoanAmount);
                        message.IsGlobal = false;
                        message.IsRead = false;
                        message.ReferenceId = order.CreatedBy;//订单所有人
                        message.CreatedBy = message.ModifiedBy = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        message.Title = "您有一笔订单放款成功！";//银行
                        message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：已放款<br/>申请金额：{2}<br/>实际放款：{3}万", order.Id, order.Code, order.LoanAmount, order.GotLoanAmount);
                        message.ReferenceId = this.User.Id;
                        this._IF_ActivityService.Create(message);

                        var user = this._IF_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
                        if (user != null)
                        {
                            message.Title = "您有一笔订单放款成功！";//平台
                            message.Content = string.Format("订单号：<a code='{0}'>{1}</a><br/>状态：已放款<br/>申请金额：{2}<br/>实际放款：{3}万", order.Id, order.Code, order.LoanAmount, order.GotLoanAmount);
                            message.ReferenceId = user.F_UserId;
                            this._IF_ActivityService.Create(message);
                        }
                    }
                    break;
                case F_OrderStatusEnum.Canceled:
                    {
                    }
                    break;

            }

        }
        
        /// <summary>
        /// 获取最新申请订单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetLatestOrder()
        {
            var list = this._IF_OrderService.GetAll(1, int.MaxValue).OrderByDescending(item => item.CreatedDate).Take(5);
            var result = new F_OrderDTOList();
            list.ToList().ForEach(item =>
            {
                item.City = item.City ?? "";
                item.Name = item.Name ?? "";
                item.PersonalPhone = item.PersonalPhone ?? "";
                item.City = item.City.Length > 3 ? string.Format("{0}**", item.Name.Substring(0, 3)) : item.City;
                item.Name = string.Format("{0}**", item.Name.Substring(0, 1));
                item.PersonalPhone = item.PersonalPhone.Length == 11 ? string.Format("{0}***{1}", item.PersonalPhone.Substring(0, 3), item.PersonalPhone.Substring(8, 3)) : item.PersonalPhone;
                result.Add(item);
            });
            return Json(new MessageResult { Status = true, Data = result });
        }
    }
}
