using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 工厂资料
    /// </summary>
    [DisplayName("工厂资料")]
    public class Base_FactoryDTO : F_ModelRoot
    {
        /// <summary>
        /// 订单Id(更新订单时使用)
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        [Required(ErrorMessage = "身份证号码为必填项")]
        public string IDNo { get; set; }

        /// <summary>
        /// 营业执照
        /// </summary>
        [DisplayName("营业执照")]
        [Required(ErrorMessage = "营业执照为必填项")]
        public string BusinessLicenseImg { get; set; }
        /// <summary>
        /// 营业执照编号
        /// </summary>
        [DisplayName("营业执照编号")]
        public string BusinessLicenseNo { get; set; }

        /// <summary>
        /// 工厂名称
        /// </summary>
        [DisplayName("工厂名称")]
        public string EntityName { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        [DisplayName("行业")]
        public string Industry { get; set; }
        /// <summary>
        /// 工所在省份
        /// </summary>
        [DisplayName("所在省份")]
        public string Province { get; set; }
        /// <summary>
        /// 工厂所在城市
        /// </summary>
        [DisplayName("所在城市")]
        public string City { get; set; }
        /// <summary>
        /// 工厂所在县区
        /// </summary>
        [DisplayName("所在县区")]
        public string Country { get; set; }
        /// <summary>
        /// 工厂所在街道
        /// </summary>
        [DisplayName("所在街道")]
        public string Street { get; set; }

        /// <summary>
        /// 工厂所在地区（省 市 县）
        /// </summary>
        [DisplayName("工厂所在地区")]
        public string FactoryAddress { get; set; }

        /// <summary>
        /// 工厂面积
        /// </summary>
        [DisplayName("工厂面积")]
        public int Area { get; set; }
        /// <summary>
        /// 年产值
        /// </summary>
        [DisplayName("年产值")]
        public double TotalAmount { get; set; }
        /// <summary>
        /// 经销商数量
        /// </summary>
        [DisplayName("经销商数量")]
        public int SubEntity { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        [DisplayName("员工数量")]
        public int HeadCount { get; set; }
    }

    public class Base_FactoryDTOList : List<Base_FactoryDTO>
    { }

}
