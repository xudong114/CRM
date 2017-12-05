using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 价格策略
    /// </summary>
    public class PriceStrategy : AggregateRoot
    {
        /// <summary>
        /// 策略名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 终止时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 标准价格
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 经销商标准价格
        /// </summary>
        public bool IsAgent { get; set; }

        public virtual ICollection<PriceStrategyDetail> PriceStrategyDetailList { get; set; }
    }

}

