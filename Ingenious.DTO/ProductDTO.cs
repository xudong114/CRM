using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class ProductDTO : ModelRoot
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        [Required(ErrorMessage = "产品名称是必填项")]
        [DisplayName("产品名称")]
        public string Name { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        [DisplayName("产品编号")]
        public string Code { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Comment { get; set; }

        /// <summary>
        /// 在线结算
        /// </summary>
        [DisplayName("在线结算")]
        public bool IsSettlement { get; set; }

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

    public class ProductDTOList:List<ProductDTO>
    { }

}
