using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Enum
{
    /// <summary>
    /// 金融账号用户类型
    /// </summary>
    public enum F_UserType
    {
        /// <summary>
        /// 消费者
        /// </summary>
        US = 2,
        /// <summary>
        /// 导购员
        /// </summary>
        CL = 4,
        /// <summary>
        /// 银行信贷经理
        /// </summary>
        BM = 8,
        /// <summary>
        /// 银行客户经理
        /// </summary>
        BC = 16,
        /// <summary>
        /// 金融客服
        /// </summary>
        CS = 32,
        /// <summary>
        /// 金融专员
        /// </summary>
        SS = 64,
        /// <summary>
        /// 商家
        /// </summary>
        ST = 128
    }
}
