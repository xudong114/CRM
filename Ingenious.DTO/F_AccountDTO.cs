using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 店员账户
    /// </summary>
    public class F_AccountDTO : F_ModelRoot
    {
        /// <summary>
        /// 支付宝账号
        /// </summary>
        [DisplayName("支付宝账号")]
        public string Alipay { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        [DisplayName("真实姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 用户Id(F_User.Id)
        /// </summary>
        [DisplayName("用户Id(F_User.Id)")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 上次收益
        /// </summary>
        [DisplayName("上次收益")]
        public decimal LastGain { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        [DisplayName("账户余额")]
        public decimal Balance { get; set; }
        /// <summary>
        /// 收益总额
        /// </summary>
        [DisplayName("收益总额")]
        public decimal TotalAmount { get; set; }
    }
}
