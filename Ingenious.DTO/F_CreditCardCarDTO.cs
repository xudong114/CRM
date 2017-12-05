using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 信用卡申请-信用卡信息
    /// </summary>
    public class F_CreditCardDTO : F_ModelRoot
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 开卡行
        /// </summary>
        [DisplayName("开卡行")]
        public string Bank { get; set; }
        /// <summary>
        /// 额度
        /// </summary>
        [DisplayName("额度")]
        public string Limit { get; set; }
    }

}
