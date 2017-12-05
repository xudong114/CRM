using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 提现记录
    /// </summary>
    [DisplayName("提现记录")]
    public class F_WithdrawDepositRecordDTO : F_ModelRoot
    {
        /// <summary>
        /// 导购员工号
        /// </summary>
        [DisplayName("导购员工号")]
        public string ClerkCode { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        [DisplayName("提现金额")]
        public decimal Money { get; set; }

        /// <summary>
        /// 交易号
        /// </summary>
        [DisplayName("交易号")]
        public string TradeNo { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
    }

    public class F_WithdrawDepositRecordDTOList : List<F_WithdrawDepositRecordDTO>
    { }
}
