using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
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
    /// 店铺管理
    /// </summary>
    public class StoreController : BaseController
    {
        private readonly IF_StoreService _IF_StorerService;
        private readonly IF_UserDetailService _IF_UserDetailService;
        public StoreController() { }

        public StoreController(IF_StoreService iF_StorerService,IF_UserDetailService iF_UserDetailService) 
        {
            this._IF_StorerService = iF_StorerService;
            this._IF_UserDetailService = iF_UserDetailService;
        }

        /// <summary>
        /// 根据店铺编号获取店铺信息
        /// </summary>
        /// <param name="code">店铺编号</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStore(string code)
        {
           var store = this._IF_StorerService.GetStoreByCode(code);

           return Json(new MessageResult { Status = true, Data = store });
        }

        /// <summary>
        /// 绑定店员和店铺
        /// </summary>
        /// <param name="storeId">店铺Id</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult BindStore(string storeCode)
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);

            var result = this._IF_StorerService.BindStore(storeCode, user.Code);

            return Json(new MessageResult { Status = result, Message = result ? "绑定成功" : "绑定失败" });
        }

        /// <summary>
        /// 根据店员Id获取店铺
        /// </summary>
        /// <param name="userId">店员Id</param>
        /// <returns>
        /// JSON数据格式：
        /// {
        ///     Status:true,
        ///     Data:F_StoreDTO
        /// }
        /// </returns>
        public IHttpActionResult GetStoreByClerkId(Guid userId)
        {
            var user = this._IF_UserDetailService.GetUserDetailByUserId(userId);

            var store = this._IF_StorerService.GetStoreByClerkId(user.Code);

            return Json(new MessageResult { Status = true, Message = "", Data = store });
        }

        /// <summary>
        /// 店铺模块：根据账号获取店铺
        /// </summary>
        /// <returns>
        /// JSON数据格式：
        /// {
        ///     Status:true,
        ///     Data:F_StoreDTO
        /// }
        /// </returns>
        public IHttpActionResult GetStore()
        {
            var store = this._IF_StorerService.GetStoreByEnglishName(this.User.UserName);

            return Json(new MessageResult { Status = true, Message = "", Data = store });
        }
        /// <summary>
        /// 店铺模块：获取店铺所有导购
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetClerks()
        {
            var store = this._IF_StorerService.GetStoreByEnglishName(this.User.UserName);

            var list = this._IF_StorerService.GetClerks(store.Code);

            return Json(new MessageResult { Status = true, Message = "", Data = GetSimpleClerks(list) });
        }

        private List<dynamic> GetSimpleClerks(List<F_UserDetailDTO> list)
        {
            var clerks =new List<dynamic>();
            foreach(var item in list)
            {
                clerks.Add(new {
                    Face = item.Face,
                    Name = item.Name,
                    Phone=item.PersonalPhone??string.Empty
                });
            }
            return clerks;
        }

    }
}
