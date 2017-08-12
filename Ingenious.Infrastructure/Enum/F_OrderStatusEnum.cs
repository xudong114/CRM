using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Enum
{
    /// <summary>
    /// 贷款申请订单状态
    /// 其中SingCanceled 、Canceled、Successed 为最终状态，不可返回修改。
    /// </summary>
    public enum F_OrderStatusEnum
    {
        /// <summary>
        /// 未提交
        /// </summary>
        [Description("未提交")]
        Temp = 0,
        /// <summary>
        /// 申请中[正在处理]
        /// </summary>
        [Description("待处理")]
        InProcess = 2,
        /// <summary>
        /// 平台通过[正在处理]
        /// </summary>
        [Description("平台通过")]
        GojiajuPassed = 4,
        /// <summary>
        /// 平台拒绝[正在处理]
        /// </summary>
        [Description("平台拒绝")]
        GojiajuDenied = 8,
        /// <summary>
        /// 银行通过[正在处理]
        /// </summary>
        [Description("银行通过")]
        BankPassed = 16,
        /// <summary>
        /// 银行拒绝[正在处理]
        /// </summary>
        [Description("银行拒绝")]
        BankDenied = 32,
        /// <summary>
        /// 银行签约[正在处理]
        /// </summary>
        [Description("银行签约")]
        BankSigned = 64,
        /// <summary>
        /// 取消签约[已拒绝]
        /// </summary>
        [Description("取消签约")]
        SignCanceled = 128,
        /// <summary>
        /// 银行放款[已通过]
        /// </summary>
        [Description("银行放款")]
        Successed = 256,
        /// <summary>
        /// 取消[已拒绝]
        /// </summary>
        [Description("取消")]
        Canceled = 512
    }
}
