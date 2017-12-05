using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 贷款产品
    /// </summary>
    public class G_LoanProduct : AggregateRoot
    {
        /// <summary>
        /// Logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 利率
        /// </summary>
        public string Rate { get; set; }
        /// <summary>
        /// 可贷金额
        /// </summary>
        public string Loan { get; set; }
        /// <summary>
        /// 还款方式
        /// </summary>
        public string Terms { get; set; }
        /// <summary>
        /// 贷款优势
        /// </summary>
        public string Tip { get; set; }

        /// <summary>
        /// 申请条件
        /// </summary>
        public string Policy { get; set; }
    }
}
