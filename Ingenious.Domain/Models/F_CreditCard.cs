
using System;
namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 信用卡申请-信用卡信息
    /// </summary>
    public class F_CreditCard : AggregateRoot
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 开卡行
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// 额度
        /// </summary>
        public string Limit { get; set; }
    }
}
