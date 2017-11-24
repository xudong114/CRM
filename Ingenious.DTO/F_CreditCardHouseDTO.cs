using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 信用卡申请-房产信息
    /// </summary>
    public class F_CreditCardHouseDTO : F_ModelRoot
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 所在小区
        /// </summary>
        [DisplayName("所在小区")]
        public string Name { get; set; }
        /// <summary>
        /// 房产地址
        /// </summary>
        [DisplayName("房产地址")]
        public string Address { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        [DisplayName("面积")]
        public string Size { get; set; }
        /// <summary>
        /// 是否全款
        /// </summary>
        [DisplayName("是否全款")]
        public bool IsLoan { get; set; }
    }
}
