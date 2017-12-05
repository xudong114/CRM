using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// GO佳居基础数据：车辆信息
    /// </summary>
    [DisplayName("GO佳居基础数据：车辆信息")]
    public class Base_CarDTO : F_ModelRoot
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        public string IDNo { get; set; }
        /// <summary>
        /// 行驶证
        /// </summary>
        [DisplayName("行驶证")]
        public string LicenseImg { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        [DisplayName("品牌")]
        public string Brand { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>
        [DisplayName("购买时间")]
        public DateTime PurchasedDate { get; set; }
        /// <summary>
        /// 是否二手车
        /// </summary>
        [DisplayName("是否二手车")]
        public bool IsSecondHand { get; set; }
        /// <summary>
        /// 估值
        /// </summary>
        [DisplayName("估值")]
        public int Valuation { get; set; }
        /// <summary>
        /// 行驶里程 vehicle-miles of travel
        /// </summary>
        [DisplayName("行驶里程")]
        public int VMT { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
    }

    public class Base_CarDTOList : List<Base_CarDTO>
    {

    }

    public class Base_CarDTODTOListWithPagination : PagedResult<Base_CarDTO>
    {

    }

}
