using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Infrastructure.Enum
{
    public enum ContractStatusEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 0,
        /// <summary>
        /// 执行中
        /// </summary>
        [Description("执行中")]
        Starting = 2,
        /// <summary>
        /// 结束
        /// </summary>
        [Description("结束")]
        Ended = 4,
        /// <summary>
        /// 意外终止
        /// </summary>
        [Description("意外终止")]
        Abort = 8
    }
}
