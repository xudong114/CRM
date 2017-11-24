using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class F_BankOptionDTO : F_ModelRoot
    {
        public Guid F_BankId { get; set; }
        public F_BankDTO F_Bank { get; set; }
        /// <summary>
        /// 利率
        /// </summary>
        [DisplayName("利率")]
        public string Rate { get; set; }
        /// <summary>
        /// 可贷金额
        /// </summary>
        [DisplayName("可贷金额")]
        public string Loan { get; set; }
        /// <summary>
        /// 可选期数
        /// </summary>
        [DisplayName("可选期数")]
        public string Terms { get; set; }
        /// <summary>
        /// 申请条件
        /// </summary>
        [DisplayName("申请条件")]
        public string Policy { get; set; }
        /// <summary>
        /// 贷款流程
        /// </summary>
        [DisplayName("贷款流程")]
        public string Workflow { get; set; }

    }
}
