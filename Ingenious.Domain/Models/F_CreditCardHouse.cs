
using System;
namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 信用卡申请-房产信息
    /// </summary>
    public class F_CreditCardHouse : AggregateRoot
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 所在小区
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 房产地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public string Size { get; set; }
        /// <summary>
        /// 是否全款
        /// </summary>
        public bool IsLoan { get; set; }
    }
}
