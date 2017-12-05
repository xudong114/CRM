using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenious.Infrastructure.Helper;

namespace Ingenious.DTO
{
    /// <summary>
    /// 订单审核记录
    /// </summary>
    [DisplayName("订单审核记录")]
    public class F_OrderRecordDTO : F_ModelRoot
    {
        /// <summary>
        /// 审核人标识(F_User.Id)
        /// </summary>
        [DisplayName("审核人标识(F_User.Id)")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 审核人工号
        /// </summary>
        [DisplayName("审核人工号")]
        public string UserCode { get; set; }
        /// <summary>
        /// 审核人姓名
        /// </summary>
        [DisplayName("审核人姓名")]
        public string UserName { get; set; }
        /// <summary>
        /// 客服电话
        /// </summary>
        [DisplayName("客服电话")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 申请单标识（F_Order.Id）
        /// </summary>
        [DisplayName("申请单标识（F_Order.Id）")]
        public Guid OrderId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        public F_OrderStatusEnum Status { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        [DisplayName("状态名称")]
        public string StatusLabel { get; set; }
        //{
        //    get
        //    {
        //        return this.Status.Discription();
        //    }
        //}
        /// <summary>
        /// 订单阶段
        /// </summary>
        [DisplayName("订单阶段")]
        public string StatusName { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [DisplayName("备注信息")]
        public string Remark { get; set; }
        /// <summary>
        /// 银行编码（分配订单使用）
        /// </summary>
        [DisplayName("银行编码")]
        public string BankCode { get; set; }

        /// <summary>
        /// 审核时间（yyyy.MM.dd hh:mm）
        /// </summary>
        [DisplayName("审核时间（yyyy.MM.dd hh:mm）")]
        public string AuditDateTime { get; set; }
        /// <summary>
        /// 放款日期（yyyy-MM-dd ）
        /// </summary>
        [DisplayName("放款日期（yyyy-MM-dd ）")]
        public string GotDate { get; set; }
        public string BankClerk { get; set; }
        public string BankManager { get; set; }

        public decimal GotLoanMoney { get; set; }
    }

    public class F_OrderRecordDTOList : List<F_OrderRecordDTO>
    { }
}
