
using System;
namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 信用卡申请-专卖店
    /// </summary>
    public class F_CreditCardStore : AggregateRoot
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 专卖店地址
        /// </summary>
        public string Address { get; set; }
    }
}
