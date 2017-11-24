using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// GO佳居基础数据：车辆信息
    /// </summary>
    public class Base_Car : AggregateRoot
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 行驶证
        /// </summary>
        public string LicenseImg { get; set; }
        /// <summary>
        /// 购买时间
        /// </summary>
        public DateTime PurchasedDate { get; set; }
        /// <summary>
        /// 是否二手车
        /// </summary>
        public bool IsSecondHand { get; set; }
        /// <summary>
        /// 估值
        /// </summary>
        public int Valuation { get; set; }
        /// <summary>
        /// 行驶里程 vehicle-miles of travel
        /// </summary>
        public int VMT { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
