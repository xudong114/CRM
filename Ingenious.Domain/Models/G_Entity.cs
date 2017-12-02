using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 佳居贷组织机构（金融代理商）
    /// </summary>
    public class G_Entity : AggregateRoot
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// 机构编码(自动生成)
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string EntityName { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }
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
        /// 法人身份证号码
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 法人身份证照片（正反面）
        /// </summary>
        public string IDImg { get; set; }
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
