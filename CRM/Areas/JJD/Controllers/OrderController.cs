using CRM.Areas.JJD.Models;
using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ingenious.Infrastructure.Extensions;
using Ingenious.Infrastructure.Cache;

namespace CRM.Areas.JJD.Controllers
{
    public class OrderController : BaseController
    {
        public OrderController() { }

        private readonly IG_OrderService _IG_OrderService;
        private readonly IBase_FactoryService _IBase_FactoryService;
        private readonly IBase_StoreService _IBase_StoreService;
        private readonly IG_LoanProductService _IG_LoanProductService;
        private readonly IG_UserService _IG_UserService;
        private readonly IG_UserDetailService _IG_UserDetailService;
        public OrderController(IG_OrderService iG_OrderService,
            IBase_FactoryService iBase_FactoryService,
            IBase_StoreService iBase_StoreService,
            IG_LoanProductService iG_LoanProductService,
            IG_UserService iG_UserService,
            IG_UserDetailService iG_UserDetailService)
        {
            this._IG_OrderService = iG_OrderService;
            this._IBase_FactoryService = iBase_FactoryService;
            this._IBase_StoreService = iBase_StoreService;
            this._IG_LoanProductService = iG_LoanProductService;
            this._IG_UserService = iG_UserService;
            this._IG_UserDetailService = iG_UserDetailService;
        }

        // GET: JJD/Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Step01(Guid? orderId = null, string product = "")
        {
            ViewBag.product = product;
            G_OrderDTO order = new G_OrderDTO();
            if (orderId.HasValue)
            {
                ViewBag.orderId = orderId;
                order = this._IG_OrderService.GetByKey(orderId.Value);
            }
            ViewBag.products = this._IG_LoanProductService.GetAll();
            return View(order);
        }

        [HttpPost]
        public JsonResult Step01(G_OrderDTO order)
        {
            if (order == null || order.LoanAmount == 0)
            {
                return Json(new MessageResult { Status = false, Message = "资料不全" }, JsonRequestBehavior.AllowGet);
            }

            ViewBag.orderId = order.Id;

            var orderId = order.Id;
            if (order.Id == Guid.Empty)
            {
                order.Code = string.Empty;
                order.CreatedBy = order.ModifiedBy = this.User.Id;
                orderId = this._IG_OrderService.Create(order).Id;
            }
            else
            {
                var oldOrder = this._IG_OrderService.GetByKey(order.Id);
                oldOrder.ModifiedBy = this.User.Id;
                oldOrder.ProductCode = order.ProductCode;
                oldOrder.LoanAmount = order.LoanAmount;
                oldOrder.ReferenceCode = order.ReferenceCode;

                this._IG_OrderService.Update(new List<G_OrderDTO> { oldOrder });
            }
            return Json(new MessageResult { Status = true, Message = "", Data = orderId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Step02(Guid orderId)
        {
            ViewBag.orderId = orderId;
            G_OrderDTO order = this._IG_OrderService.GetByKey(orderId);
            return View(order);
        }

        [HttpPost]
        public ActionResult Step02(G_OrderDTO order, string SecurityCode, string IDNoFaceImg, string IDNoBackImg)
        {
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "资料不全" });
            }
            ViewBag.orderId = order.Id;
            if (string.IsNullOrWhiteSpace(order.Name)
                || string.IsNullOrWhiteSpace(order.PersonalPhone)
                || string.IsNullOrWhiteSpace(order.IDNo))
            {
                return Json(new MessageResult { Status = false, Message = "资料不全" });
            }
            var oldOrder = this._IG_OrderService.GetByKey(order.Id);
            if (string.IsNullOrWhiteSpace(oldOrder.PersonalPhone))
            {
                var oldCode = CacheService.Instance.Get(order.PersonalPhone, order.PersonalPhone);
                if (oldCode == null)
                {
                    return Json(new MessageResult { Status = false, Message = "验证码失效" });
                }

                if (!oldCode.ToString().Equals(SecurityCode))
                {
                    return Json(new MessageResult { Status = false, Message = "验证码错误" });
                }
            }
            oldOrder.ModifiedBy = this.User.Id;
            oldOrder.Name = order.Name;
            oldOrder.IDNo = order.IDNo;
            oldOrder.PersonalPhone = order.PersonalPhone;
            oldOrder.IDImg = order.IDImg;
            this._IG_OrderService.Update(new List<G_OrderDTO> { oldOrder });

            return Json(new MessageResult { Status = true, Message = "", Data = oldOrder.Id });
        }

        public ActionResult Step03(Guid orderId)
        {
            ViewBag.orderId = orderId;
            G_OrderDTO order = this._IG_OrderService.GetByKey(orderId);
            return View(order);
        }

        [HttpPost]
        public ActionResult Step03(G_OrderDTO order)
        {
            if (order == null)
            {
                return Json(new MessageResult { Status = false, Message = "资料不全" });
            }

            ViewBag.orderId = order.Id;

            if (order.HasHouse)
            {
                if (string.IsNullOrWhiteSpace(order.Province)
                    //|| string.IsNullOrWhiteSpace(order.City)
                    //|| string.IsNullOrWhiteSpace(order.Country)
                    || string.IsNullOrWhiteSpace(order.Community))
                {
                    return Json(new MessageResult { Status = false, Message = "请填写完整房产资料" });
                }
            }

            var model = this._IG_OrderService.GetByKey(order.Id);
            if (model == null)
            {
                return Json(new MessageResult { Status = false, Message = "无效订单" });
            }

            model.Province = order.Province;
            model.City = order.City;
            model.Country = order.Country;
            model.Community = order.Community;
            model.Area = order.Area;
            model.HasHouse = order.HasHouse;

            model.ModifiedBy = this.User.Id;
            this._IG_OrderService.Update(new List<G_OrderDTO> { model });

            return Json(new MessageResult { Status = true, Message = "", Data = model.Id });
        }

        public ActionResult Step04(Guid orderId)
        {
            ViewBag.orderId = orderId;

            var model = new OrderComplexModel();
            model.Order = this._IG_OrderService.GetByKey(orderId);
            if (string.IsNullOrWhiteSpace(model.Order.IDNo))
            {
                model.Factory = new Base_FactoryDTO();
                model.Stores = new List<Base_StoreDTO>();
            }
            else
            {
                model.Factory = this._IBase_FactoryService.GetFactoryByCode(model.Order.IDNo) ?? new Base_FactoryDTO();
                model.Stores = this._IBase_StoreService.GetStoreByCode(model.Order.IDNo);
            }

            return View(model);
        }

        public ActionResult Success()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CreateFactory(Base_FactoryDTO factory)
        {
            if (ModelState.IsValid)
            {
                if (factory.Id != Guid.Empty)
                {
                    factory.ModifiedBy = this.User.Id;
                    this._IBase_FactoryService.Update(new List<Base_FactoryDTO> { factory });
                }
                else
                {
                    this._IBase_FactoryService.Create(factory);
                }

                //更新订单状态：在处理
                var order = this._IG_OrderService.GetByKey(factory.OrderId);
                order.Status = Ingenious.Infrastructure.Enum.G_OrderStatusEnum.PreProcess;
                order.IsFactory = true;

                //如果是新创建订单，则进行订单分配。
                if (string.IsNullOrWhiteSpace(order.GoJiajuClerkCode))
                {
                    //绑定金融客服
                    //1、如果未填写推广码，则订单分配默认客服（艾博账号）
                    if (string.IsNullOrWhiteSpace(order.ReferenceCode))
                    {
                        var defaultAccount = System.Configuration.ConfigurationManager.AppSettings["default:account"].ToString();
                        order.GoJiajuClerkCode = defaultAccount;
                    }
                    else
                    {
                        //2、如果填写推广码，则分配到该金融专员所在机构的金融客服账号
                        var referenceUser = this._IG_UserService.GetGojiajuClerkByReferenceCode(order.ReferenceCode);
                        if (referenceUser != null)
                        {
                            order.GoJiajuClerkCode = referenceUser.G_UserDetail.Code;
                        }
                        else
                        {
                            return Json(new MessageResult { Status = false, Message = "金融专员账号验证失败" });
                        }
                    }
                }

                this._IG_OrderService.Update(new List<G_OrderDTO> { order });
                this.Notify(order);
                return Json(new MessageResult { Status = true, Message = "保存成功" });
            }
            return Json(new MessageResult { Status = false, Message = ModelState.GetErrorMsg() });
        }

        [HttpPost]
        public JsonResult CreateStores(OrderComplexModel order)
        {
            if (order == null
                || order.Stores == null)
            {
                return Json(new MessageResult { Status = true });
            }
            order.Order = this._IG_OrderService.GetByKey(order.OrderId);
            var listUpdate = new List<Base_StoreDTO>();
            foreach (var item in order.Stores)
            {
                if (string.IsNullOrWhiteSpace(item.Brand)
                    && string.IsNullOrWhiteSpace(item.Industry)
                    && string.IsNullOrWhiteSpace(item.Province)
                    && string.IsNullOrWhiteSpace(item.City)
                    && string.IsNullOrWhiteSpace(item.Mall)
                    && string.IsNullOrWhiteSpace(item.Area)
                    && string.IsNullOrWhiteSpace(item.BusinessLicenseImg)
                    )
                {
                    continue;
                }

                if (order.Order != null)
                {
                    item.IDNo = order.Order.IDNo;
                    //item.PersonalPhone = order.Order.PersonalPhone;
                    item.StoreName = order.Order.Name;
                }
                item.ModifiedBy = this.User.Id;
                if (item.Id != Guid.Empty)
                {
                    listUpdate.Add(item);
                }
                else
                {
                    item.CreatedBy = this.User.Id;
                    this._IBase_StoreService.Create(item);
                }
            }

            this._IBase_StoreService.Update(listUpdate);

            //更新订单状态：在处理
            var model = this._IG_OrderService.GetByKey(order.OrderId);

            //绑定金融客服
            //如果是新创建订单，则进行订单分配。
            if (string.IsNullOrWhiteSpace(order.Order.GoJiajuClerkCode))
            {
                //1、如果未填写推广码，则订单分配默认客服（艾博账号）
                if (string.IsNullOrWhiteSpace(model.ReferenceCode))
                {
                    var defaultAccount = System.Configuration.ConfigurationManager.AppSettings["default:account"].ToString();
                    model.GoJiajuClerkCode = defaultAccount;
                }
                else
                {
                    //2、如果填写推广码，则分配到该金融专员所在机构的金融客服账号
                    var referenceUser = this._IG_UserService.GetGojiajuClerkByReferenceCode(model.ReferenceCode);
                    if (referenceUser != null)
                    {
                        model.GoJiajuClerkCode = referenceUser.G_UserDetail.Code;
                    }
                    else
                    {
                        return Json(new MessageResult { Status = false, Message = "金融专员账号验证失败" });
                    }
                }
            }

            model.Status = Ingenious.Infrastructure.Enum.G_OrderStatusEnum.PreProcess;
            this._IG_OrderService.Update(new List<G_OrderDTO> { model });

            return Json(new MessageResult { Status = true });
        }

        public JsonResult RemoveStore(Guid id)
        {
            var store = this._IBase_StoreService.GetByKey(id);
            store.IsActive = false;
            this._IBase_StoreService.Delete(new List<Base_StoreDTO> { store });
            return Json(new MessageResult { Status = true, Message = "" });
        }

        private void Notify(G_OrderDTO order)
        {
            //1、申请人
            Ingenious.Infrastructure.Message.MessageService.SMSSend(order.PersonalPhone,string.Format("恭喜您成功刚提交了一笔贷款申请，稍后会有客服和您联系。订单号：{0}",order.Code));
            //2、金融客服
            
            var goJiajuClerkCode = this._IG_UserDetailService.GetUserDetailByCode(order.GoJiajuClerkCode);
            if (goJiajuClerkCode != null && !string.IsNullOrWhiteSpace(goJiajuClerkCode.G_User.UserName))
            {
                Ingenious.Infrastructure.Message.MessageService.SMSSend(goJiajuClerkCode.G_User.UserName, string.Format("您有一笔贷款申请需要处理，请登录系统查看。订单号：{0}", goJiajuClerkCode.PersonalPhone));
            }
        }

    }
}