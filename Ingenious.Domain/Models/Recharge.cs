using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 经销商账单
    /// </summary>
    public class Recharge : AggregateRoot
    {
        /// <summary>
        /// 关联账号标识
        /// </summary>
        public Guid AccountId { get; set; }
        /// <summary>
        /// 关联账号
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// 消费金额
        /// </summary>
        public decimal Money { get; set; }

    }
}
