using Ingenious.Domain.Models;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.DataSource
{
    /// <summary>
    /// 订单查询数据结构
    /// </summary>
    public class ComplexOrder : AggregateRoot
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Code { get; set; }

        #region 个人信息
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PersonalPhone { get; set; }

        #endregion

        #region 房产信息
        /// <summary>
        /// 房产所在省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 房产所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 房产所在县区
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 房产所在小区
        /// </summary>
        public string Community { get; set; }
        /// <summary>
        /// 房产户主，与房产证上户主姓名一致
        /// </summary>
        public string Landlord { get; set; }
        /// <summary>
        /// 是否分期付款
        /// </summary>
        public bool IsInstallment { get; set; }
        /// <summary>
        /// 贷款银行标识（F_Bank.Code）,非必填项，根据是否分期来决定。
        /// </summary>
        public string FromBank { get; set; }

        #endregion

        #region 交易信息
        /// <summary>
        /// 店铺编码
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 导购编码
        /// </summary>
        public string ClerkCode { get; set; }

        /// <summary>
        /// 导购姓名
        /// </summary>
        public string ClerkName { get; set; }

        /// <summary>
        /// 客服工号
        /// </summary>
        public string GoJiajuClerkCode { get; set; }
        /// <summary>
        /// 客服电话
        /// </summary>
        public string GoJiajuClerkOfficePhone { get; set; }
        /// <summary>
        /// 客服姓名
        /// </summary>
        public string GoJiajuClerkName { get; set; }

        /// <summary>
        /// 交易总额（万元）
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 首付金额
        /// </summary>
        public decimal DownpaymentAmount { get; set; }
        /// <summary>
        /// 申请贷款金额
        /// </summary>
        public decimal LoanAmount { get; set; }
        /// <summary>
        /// 实际发放金额
        /// </summary>
        public decimal GotLoanAmount { get; set; }
        #endregion

        /// <summary>
        /// 订单状态
        /// </summary>
        public F_OrderStatusEnum Status { get; set; }
        /// <summary>
        /// 审核银行名称
        /// </summary>
        public string BankName { get; set; }

        public string BankClerk { get; set; }
        public string BankManager { get; set; }
        public string BankClerkName { get; set; }
        public string BankManagerName { get; set; }
        public string BankCode { get; set; }
    }
}
