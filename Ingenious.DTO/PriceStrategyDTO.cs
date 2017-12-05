using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class PriceStrategyDTO : ModelRoot
    {
        /// <summary>
        /// 策略名称
        /// </summary>
        [Required(ErrorMessage = "策略名称是必填项")]
        [DisplayName("策略名称")]
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Comment { get; set; }
        /// <summary>
        /// 生效时间
        /// </summary>
        [Required(ErrorMessage = "生效时间是必填项")]
        [DisplayName("生效时间")]
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 终止时间
        /// </summary>
        [Required(ErrorMessage = "终止时间是必填项")]
        [DisplayName("终止时间")]
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 标准价格
        /// </summary>
        [DisplayName("设为标准价格")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// 经销商标准价格
        /// </summary>
        [DisplayName("设为经销商标准价格")]
        public bool IsAgent { get; set; }
        /// <summary>
        /// 价格明细
        /// </summary>
        public PriceStrategyDetailDTOList PriceStrategyDetailList { get; set; }
    }

    public class PriceStrategyDTOList:List<PriceStrategyDTO>
    { }
}
