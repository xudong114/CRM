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
    public enum G_OrderStatusEnum
    {
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Discard = -1,

        /// <summary>
        /// 未提交
        /// </summary>
        [Description("未提交")]
        Temp = 0,

        /// <summary>
        /// 预处理[正在处理]
        /// </summary>
        [Description("待审核")]
        PreProcess=2,

        /// <summary>
        /// 预处理拒绝[正在处理]
        /// </summary>
        [Description("平台拒绝")]
        PreDenied = 4,

        /// <summary>
        /// 预处理通过[正在处理]
        /// </summary>
        [Description("平台通过")]
        PrePassed = 8,

        /// <summary>
        /// 申请中[正在处理]
        /// </summary>
        [Description("待分配")]
        InProcess = 16,
        /// <summary>
        /// 平台通过[正在处理]
        /// </summary>
        [Description("已分配")]
        GojiajuPassed = 32,
        /// <summary>
        /// 平台拒绝[正在处理]
        /// </summary>
        [Description("分配失败")]
        GojiajuDenied = 64,
        /// <summary>
        /// 银行通过[正在处理]
        /// </summary>
        [Description("银行通过")]
        BankPassed = 128,
        /// <summary>
        /// 银行拒绝[正在处理]
        /// </summary>
        [Description("银行拒绝")]
        BankDenied = 256,
        /// <summary>
        /// 银行签约[正在处理]
        /// </summary>
        [Description("银行签约")]
        BankSigned = 512,
        /// <summary>
        /// 取消签约[已拒绝]
        /// </summary>
        [Description("取消签约")]
        SignCanceled = 1024,
        /// <summary>
        /// 银行放款[已通过]
        /// </summary>
        [Description("银行放款")]
        Successed = 2048,
        /// <summary>
        /// 取消[已拒绝]
        /// </summary>
        [Description("取消")]
        Canceled = 4096
    }
}
