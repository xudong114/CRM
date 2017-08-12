using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 提现记录
    /// </summary>
    public class F_WithdrawDepositRecord : AggregateRoot
    {
        /// <summary>
        /// 导购员工号
        /// </summary>
        public string ClerkCode { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 交易号
        /// </summary>
        public string TradeNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
