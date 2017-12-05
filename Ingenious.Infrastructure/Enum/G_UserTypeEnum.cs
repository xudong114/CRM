using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Enum
{
    /// <summary>
    /// 金融账号用户类型(佳居贷)
    /// </summary>
    public enum G_UserTypeEnum
    {
        /// <summary>
        /// 消费者
        /// </summary>
        [Description("消费者")]
        US = 2,
        /// <summary>
        /// 金融经理
        /// </summary>
        [Description("金融经理")]
        CL = 4,
        /// <summary>
        /// 银行信贷经理
        /// </summary>
        [Description("银行信贷经理")]
        BM = 8,
        /// <summary>
        /// 银行客户经理
        /// </summary>
        [Description("银行客户经理")]
        BC = 16,
        /// <summary>
        /// 金融客服
        /// </summary>
        [Description("金融客服")]
        CS = 32,
        /// <summary>
        /// 金融专员
        /// </summary>
        [Description("金融专员")]
        SS = 64,
        /// <summary>
        /// 商家
        /// </summary>
        [Description("商家")]
        ST = 128
    }
}
