using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.DataSource
{
    /// <summary>
    /// 订单各状态的数量
    /// </summary>
    public class ComplexStatusCount
    {
        /// <summary>
        /// 未提交数量
        /// </summary>
        public int TempCount { get; set; }
        /// <summary>
        /// 预处理[正在处理]数量
        /// </summary>
        public int PreProcess { get; set; }
        /// <summary>
        /// 申请中[正在处理]数量
        /// </summary>
        public int InProcessCount { get; set; }
        /// <summary>
        /// 平台通过[正在处理]数量
        /// </summary>
        public int GojiajuPassedCount { get; set; }
        /// <summary>
        /// 平台拒绝[正在处理]数量
        /// </summary>
        public int GojiajuDeniedCount { get; set; }
        /// <summary>
        /// 银行通过[正在处理]数量
        /// </summary>
        public int BankPassedCount { get; set; }
        /// <summary>
        /// 银行拒绝[正在处理]数量
        /// </summary>
        public int BankDeniedCount { get; set; }
        /// <summary>
        /// 银行签约[正在处理]数量
        /// </summary>
        public int BankSignedCount { get; set; }
        /// <summary>
        /// 取消签约[已拒绝]数量
        /// </summary>
        public int SignCanceledCount { get; set; }
        /// <summary>
        /// 银行放款[已通过]数量
        /// </summary>
        public int SuccessedCount { get; set; }
        /// <summary>
        /// 取消[已拒绝]数量
        /// </summary>
        public int CanceledCount { get; set; }
    }
}
