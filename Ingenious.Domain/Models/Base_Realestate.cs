using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// GO佳居基础数据：不动产信息
    /// </summary>
    public class Base_Realestate : AggregateRoot
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDNo { get; set; }

        /// <summary>
        /// 房产证照片
        /// </summary>
        public string RealestateImg { get; set; }

        /// <summary>
        /// 小区名称
        /// </summary>
        public string Community { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 房产类型
        /// 高层、洋房、别墅、学区房
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// 房产估值
        /// </summary>
        public int Valuation { get; set; }

        /// <summary>
        /// 所在省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 所在县区
        /// </summary>
        public string Country { get; set; }


    }
}
