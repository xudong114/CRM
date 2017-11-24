using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 店铺资料
    /// 关联GO佳居店铺
    /// </summary>
    public class Base_StoreDTO : F_ModelRoot
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        public string IDNo { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        [DisplayName("店铺名称")]
        public string StoreName { get; set; }

        /// <summary>
        /// 店铺编号
        /// </summary>
        [DisplayName("店铺编码")]
        public string Code { get; set; }


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
        /// 行业
        /// </summary>
        [DisplayName("行业")]
        public string Industry { get; set; }
        /// <summary>
        /// 经营品牌
        /// </summary>
        [DisplayName("经营品牌")]
        public string Brand{ get; set; }
        /// <summary>
        /// 品牌标识
        /// </summary>
        [DisplayName("品牌标识")]
        public Guid BrandId { get; set; }

        /// <summary>
        /// 所属商场
        /// </summary>
        [DisplayName("所属商场")]
        public string Mall { get; set; }
        /// <summary>
        /// 商场标识
        /// </summary>
        [DisplayName("商场标识")]
        public Guid MallId { get; set; }
        /// <summary>
        /// 门店面积
        /// </summary>
        [DisplayName("门店面积")]
        public string Area { get; set; }
        /// <summary>
        /// 门店照片
        ///  格式：图片1|图片2|图片3，使用“|”分割。
        /// </summary>
        [DisplayName("门店照片")]
        public string StoreImg { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        [DisplayName("营业执照")]
        public string BusinessLicenseImg { get; set; }
        /// <summary>
        /// 营业执照编号
        /// </summary>
        [DisplayName("营业执照编号")]
        public string BusinessLicenseNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
    }
}
