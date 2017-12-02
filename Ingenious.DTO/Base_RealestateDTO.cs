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
    /// GO佳居基础数据：不动产信息
    /// </summary>
    [DisplayName("GO佳居基础数据：不动产信息")]
    public class Base_RealestateDTO : F_ModelRoot
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        public string IDNo { get; set; }
        /// <summary>
        /// 房产证照片
        /// </summary>
        [DisplayName("房产证照片")]
        public string LicenseImg { get; set; }
        /// <summary>
        /// 小区名称
        /// </summary>
        [DisplayName("小区名称")]
        public string Community { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        [DisplayName("面积")]
        public int Area { get; set; }
        /// <summary>
        /// 房产类型
        /// 高层、洋房、别墅、学区房
        /// </summary>
        [DisplayName("房产类型")]
        public string Category { get; set; }
        /// <summary>
        /// 房产估值
        /// </summary>
        [DisplayName("房产估值")]
        public int Valuation { get; set; }
        /// <summary>
        /// 所在省份
        /// </summary>
        [DisplayName("所在省份")]
        public string Province { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        [DisplayName("所在城市")]
        public string City { get; set; }
        /// <summary>
        /// 所在县区
        /// </summary>
        [DisplayName("所在县区")]
        public string Country { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        [DisplayName("街道")]
        public string Street { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
    }

    public class Base_RealestateDTOList : List<Base_RealestateDTO>
    {

    }

    public class Base_RealestateDTODTOListWithPagination : PagedResult<Base_RealestateDTO>
    {

    }

}
