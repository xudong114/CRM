using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 信用卡申请-专卖店
    /// </summary>
    public class F_CreditCardStoreDTO : F_ModelRoot
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 专卖店地址
        /// </summary>
        [DisplayName("专卖店地址")]
        public string Address { get; set; }
    }
}
