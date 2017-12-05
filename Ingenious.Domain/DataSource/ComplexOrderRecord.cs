using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenious.Infrastructure.Helper;

namespace Ingenious.Domain.DataSource
{
    /// <summary>
    /// 订单查看数据结构
    /// </summary>
    public class ComplexOrderRecord : AggregateRoot
    {
        /// <summary>
        /// 审核人标识(F_User.Id)
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 审核人工号
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 审核人姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 客服电话
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// 申请单标识（F_Order.Id）
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public G_OrderStatusEnum Status { get; set; }

        /// <summary>
        /// 订单阶段
        /// </summary>
        public string StatusName
        {
            get
            {
                string name = string.Empty;
                switch(this.Status)
                {
                    case G_OrderStatusEnum.GojiajuPassed:
                    case G_OrderStatusEnum.GojiajuDenied:
                        {
                            name = "平台受理";
                        }
                        break;
                    case G_OrderStatusEnum.BankDenied:
                    case G_OrderStatusEnum.BankPassed:
                        {
                            name = "银行受理";
                        }
                        break;
                    case G_OrderStatusEnum.BankSigned:
                    case G_OrderStatusEnum.SignCanceled:
                        {
                            name = "银行签约";
                        }
                        break;
                    case G_OrderStatusEnum.Successed:
                        {
                            name = "银行放款";
                        }
                        break;
                }
                return name;
            }
        }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusLabel
        {
            get
            {
                return this.Status.Discription();
            }
        }
        /// <summary>
        /// 审核时间（yyyy.MM.dd hh:mm）
        /// </summary>
        public string AuditDateTime
        {
            get
            {
                return this.CreatedDate.ToString("yyyy.MM.dd hh:mm");
            }
        }
        /// <summary>
        /// 放款日期（yyyy-MM-dd ）
        /// </summary>
        public string GotDate
        {
            get
            {
                return this.CreatedDate.ToString("yyyy-MM-dd");
            }
        }
    }
}
