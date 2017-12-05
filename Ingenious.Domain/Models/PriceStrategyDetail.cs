using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 价格策略明细
    /// </summary>
    public class PriceStrategyDetail : AggregateRoot
    {
        /// <summary>
        /// 策略标识
        /// </summary>
        public Guid PriceStrategyId { get; set; }
        /// <summary>
        /// 价格策略
        /// </summary>
        public virtual PriceStrategy PriceStrategy { get; set; }
        /// <summary>
        /// 产品标识
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public virtual Product Product { get; set; }
        /// <summary>
        /// 最小购买量
        /// </summary>
        public int Minimum { get; set; }
        /// <summary>
        /// 执行价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 续费价格
        /// </summary>
        public double RenewPrice { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        public double DiscountRate { get; set; }

    }
}
