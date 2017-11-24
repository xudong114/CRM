using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// GO佳居基础数据：工厂信息
    /// </summary>
    public class Base_Factory : AggregateRoot
    {
        public Base_Factory()
        {
            this.IDNo = string.Empty;
            this.BusinessLicenseImg = string.Empty;
            this.BusinessLicenseNo = string.Empty;
            this.EntityName = string.Empty;
            this.Id = Guid.Empty;
            this.IDNo = string.Empty;
            this.Industry = string.Empty;
            this.Area = 0;
            this.SubEntity = 0;
            this.TotalAmount = 0.00f;
            this.HeadCount = 0;
            this.Province = string.Empty;
            this.City = string.Empty;
            this.Country = string.Empty;
            this.Street = string.Empty;
        }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string EntityName { get; set; }

        /// <summary>
        /// 营业执照
        /// </summary>
        public string BusinessLicenseImg { get; set; }

        /// <summary>
        /// 营业执照编号
        /// </summary>
        public string BusinessLicenseNo { get; set; }

        /// <summary>
        /// 行业
        /// </summary>
        public string Industry { get; set; }
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
        /// 所在街道
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 工厂面积
        /// </summary>
        public int Area { get; set; }
        /// <summary>
        /// 年产值
        /// </summary>
        public double TotalAmount { get; set; }
        /// <summary>
        /// 人数
        /// </summary>
        public int HeadCount { get; set; }
        /// <summary>
        /// 经销商数量
        /// </summary>
        public int SubEntity { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

}
