using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    public class F_BankOption : AggregateRoot
    {
        public Guid F_BankId { get; set; }
        public virtual F_Bank F_Bank { get; set; }
        /// <summary>
        /// 利率
        /// </summary>
        public string Rate { get; set; }
        /// <summary>
        /// 可贷金额
        /// </summary>
        public string Loan { get; set; }
        /// <summary>
        /// 可选期数
        /// </summary>
        public string Terms { get; set; }
        /// <summary>
        /// 申请条件
        /// </summary>
        public string Policy { get; set; }
        /// <summary>
        /// 贷款流程
        /// </summary>
        public string Workflow { get; set; }

    }
}
