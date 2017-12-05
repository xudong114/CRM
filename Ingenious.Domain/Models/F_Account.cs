using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 店员账户
    /// </summary>
    public class F_Account : AggregateRoot
    {
        /// <summary>
        /// 支付宝账号
        /// </summary>
        public string Alipay { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 用户Id(F_User.Id)
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 收益总额
        /// </summary>
        public decimal TotalAmount { get; set; }
    }
}
