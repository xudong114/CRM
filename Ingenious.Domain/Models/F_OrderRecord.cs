using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 订单审核记录
    /// </summary>
    public class F_OrderRecord : AggregateRoot
    {
        /// <summary>
        /// 审核人标识(F_User.Id)
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 申请单标识（F_Order.Id）
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public F_OrderStatusEnum Status { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string  Remark { get; set; }
    }
}
