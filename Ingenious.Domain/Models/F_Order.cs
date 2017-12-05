using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 贷款申请单
    /// </summary>
    public class F_Order : AggregateRoot
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
        public string  Community { get; set; }
        /// <summary>
        /// 房产户主，与房产证上户主姓名一致
        /// </summary>
        public string Landlord { get; set; }
        /// <summary>
        /// 是否分期付款
        /// </summary>
        public bool IsInstallment { get; set; }
        /// <summary>
        /// 贷款银行标识（Code）,非必填项，根据是否分期来决定。
        /// </summary>
        public string FromBank { get; set; }

        #endregion

        #region 交易信息
        /// <summary>
        /// 店铺编码
        /// </summary>
        public string  StoreCode { get; set; }
        /// <summary>
        /// 导购编码
        /// </summary>
        public string  ClerkCode { get; set; }
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

        #region 上传图片
        //1、交易凭证
        //2、授信凭证
        #endregion 

        #region 银行审批
        /// <summary>
        /// 审批银行
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 信贷经理
        /// </summary>
        public string BankManager { get; set; }
        /// <summary>
        /// 客户经理
        /// </summary>
        public string BankClerk { get; set; }

        #endregion
        /// <summary>
        /// 订单状态
        /// </summary>
        public F_OrderStatusEnum Status { get; set; }

        /// <summary>
        /// 客服工号
        /// </summary>
        public string GoJiajuClerkCode { get; set; }
    }
}
