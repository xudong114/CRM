using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Enum
{
    public enum PaymentMethodEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 0,
        /// <summary>
        /// 支票
        /// </summary>
        [Description("支票")]
        Check = 2,
        /// <summary>
        /// 现金
        /// </summary>
        [Description("现金")]
        Cash = 4,
        /// <summary>
        /// 银行转账
        /// </summary>
        [Description("银行转账")]
        Bank = 8,
        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other = 16
    }
}
