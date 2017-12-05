using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class PriceStrategyDetailDTO : ModelRoot
    {
        /// <summary>
        /// 策略标识
        /// </summary>
        [DisplayName("策略标识")]
        public Guid PriceStrategyId { get; set; }
        /// <summary>
        /// 价格策略
        /// </summary>
        public PriceStrategyDTO PriceStrategy { get; set; }
        /// <summary>
        /// 产品标识
        /// </summary>
        [DisplayName("产品标识")]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public ProductDTO Product { get; set; }
        /// <summary>
        /// 最小购买量
        /// </summary>
        [DisplayName("最小购买量")]
        public int Minimum { get; set; }
        /// <summary>
        /// 执行价格
        /// </summary>
        [DisplayName("执行价格")]
        public double Price { get; set; }
        /// <summary>
        /// 续费价格
        /// </summary>
        [DisplayName("续费价格")]
        public double RenewPrice { get; set; }
        /// <summary>
        /// 折扣率
        /// </summary>
        [DisplayName("折扣率")]
        public double DiscountRate { get; set; }
    }

    public class PriceStrategyDetailDTOList:List<PriceStrategyDetailDTO>
    { }

}
