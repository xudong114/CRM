
using System;
namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 信用卡申请-车辆信息
    /// </summary>
    public class F_CreditCardCar : AggregateRoot
    {
        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 车辆品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 估值
        /// </summary>
        public string Valuation { get; set; }
    }
}
