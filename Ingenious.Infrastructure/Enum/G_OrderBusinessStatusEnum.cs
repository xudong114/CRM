using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Enum
{
    /// <summary>
    /// 贷款申请订单业务状态
    /// </summary>
    public enum G_OrderBusinessStatusEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        Other = -1,

        /// <summary>
        /// 待受理
        /// </summary>
        [Description("待受理")]
        PreProcess = 0,

        /// <summary>
        /// 待签约
        /// </summary>
        [Description("待签约")]
        PreSign = 2,

        /// <summary>
        /// 待放款
        /// </summary>
        [Description("待放款")]
        PreSuccess = 4,

        /// <summary>
        /// 已放款
        /// </summary>
        [Description("已放款")]
        Successed = 8,

        /// <summary>
        /// 已失效
        /// </summary>
        [Description("已失效")]
        Canceled = 16
    }
}
