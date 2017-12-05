using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 信用卡申请-车辆信息
    /// </summary>
    public class F_CreditCardCarDTO : F_ModelRoot
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 车辆品牌
        /// </summary>
        [DisplayName("车辆品牌")]
        public string Brand { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        [DisplayName("型号")]
        public string Type { get; set; }
        /// <summary>
        /// 估值
        /// </summary>
        [DisplayName("估值")]
        public string Valuation { get; set; }
    }

}
