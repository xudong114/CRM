using Ingenious.Application.Interface;
using Ingenious.DTO;
using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Go.Controllers
{
    /// <summary>
    /// 银行模块服务
    /// </summary>
    public class BankController : BaseController
    {

        private readonly IF_UserDetailService _IF_UserDetailService;
        private readonly IF_UserService _IF_UserService;
        private readonly IF_BankService _IF_BankService;
        public BankController(IF_UserDetailService iF_UserDetailService,
            IF_UserService iF_UserService,
            IF_BankService iF_BankService)
        {
            this._IF_UserDetailService = iF_UserDetailService;
            this._IF_UserService = iF_UserService;
            this._IF_BankService = iF_BankService;
        }

        /// <summary>
        /// 获取贷款银行
        /// 总行
        /// </summary>
        /// <returns>
        /// 返回JSON数据结构
        /// {
        ///     Status:true,
        ///     Data:{
        ///             Name: 银行名称
        ///             Code: 银行编码
        ///         }
        /// }
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetBanks()
        {
           var list = this._IF_BankService.GetBanks(null, true);
           return Json(new MessageResult { Status = true, Data = list });
        }

        /// <summary>
        /// 获取订单分配银行
        /// </summary>
        /// <returns>
        /// 返回JSON数据结构
        /// {
        ///     Status:true,
        ///     Data:{
        ///             Name: 银行名称
        ///             Code: 银行编码
        ///         }
        /// }
        /// </returns>
        [HttpGet]
        public IHttpActionResult GetOrderBanks()
        {
            var list = this._IF_BankService.GetBanks(null,false);
            return Json(new MessageResult { Status = true, Data = list });
        }

        /// <summary>
        /// 银行模块：获取所有客户经理。
        ///         根据当前登录信贷经理账号Id获取所在银行的所有客户经理。
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetClerks()
        {
            if (this.User.UserType != Ingenious.Infrastructure.Enum.F_UserTypeEnum.BM)
            {
                return Json(new MessageResult { Status = false, Message = "对该请求的授权已被拒绝" });
            }

            var user = this._IF_UserDetailService.GetUserDetailByUserId(this.User.Id);
            if (string.IsNullOrWhiteSpace(user.BankCode))
            {
                return Json(new MessageResult { Status = false, Message = "所属银行未设置" });
            }

            var list = this._IF_UserDetailService.GetUserDetailByBankCode(user.BankCode)
                .Where(item => item.F_UserId != user.F_UserId
                    && item.F_User.UserType == Ingenious.Infrastructure.Enum.F_UserTypeEnum.BC)
                .ToList();

            return Json(new MessageResult { Status = true, Data = GetSimpleClerks(list) });
        }
        
        private List<dynamic> GetSimpleClerks(List<F_UserDetailDTO> list)
        {
            var clerks = new List<dynamic>();
            foreach (var item in list.OrderByDescending(item=>item.F_User.IsActive).ThenByDescending(item=>item.CreatedDate))
            {
                clerks.Add(new
                {
                    Id = item.Id,
                    UserId = item.F_UserId,
                    Face = item.Face,
                    Name = item.Name,
                    PersonalPhone = item.PersonalPhone,
                    OfficePhone = item.OfficePhone??string.Empty,
                    Status = item.F_User.IsActive,
                    Code = item.Code
                });
            }
            return clerks;
        }

        /// <summary>
        /// 设置分配策略
        /// 1、自动
        /// 2、手工
        /// </summary>
        /// <param name="isauto">是否开启自动分配</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult SetAssignPolicy(bool isauto)
        {
            var bank = this._IF_BankService.GetBankByUserId(this.User.Id);
            if(bank==null)
            {
                return Json(new MessageResult {  Status=false,Message="所属银行未设置"});
            }
            bank.IsAssignAuto = isauto;
            this._IF_BankService.Update(new List<F_BankDTO> { bank });
            return Json(new MessageResult { Status = false, Message = "设置成功" });
        }
    }
}
