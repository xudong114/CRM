using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// GO佳居基础数据：店铺信息
    /// </summary>
    public class Base_Store : AggregateRoot
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string StoreName { get; set; }
        /// <summary>
        /// 店铺编码
        /// </summary>
        public string Code { get; set; }

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

        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public string Industry { get; set; }

        /// <summary>
        /// 经营品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 品牌标识
        /// </summary>
        public Guid BrandId { get; set; }

        /// <summary>
        /// 所属商场
        /// </summary>
        public string Mall { get; set; }
        /// <summary>
        /// 商场标识
        /// </summary>
        public Guid MallId { get; set; }

        /// <summary>
        /// 门店面积
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 门店照片
        /// 格式：图片1|图片2|图片3，使用“|”分割。
        /// </summary>
        public string StoreImg { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string BusinessLicenseImg { get; set; }
        /// <summary>
        /// 营业执照编号
        /// </summary>
        public string BusinessLicenseNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
