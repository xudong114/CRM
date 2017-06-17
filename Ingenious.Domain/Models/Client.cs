using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 客户
    /// </summary>
    public class Client : AggregateRoot
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所属账号
        /// </summary>
        public Guid? UserId { get; set; }
        public virtual User User { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public Guid? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public Guid? GradeId { get; set; }
        public virtual Dictionary Grade { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        public Guid? IndustryId { get; set; }
        public virtual Dictionary Industry { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string Postcode { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }
        /// <summary>
        /// 公司网址
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// 微博
        /// </summary>
        public string Weibo { get; set; }
        /// <summary>
        /// 微信公众号
        /// </summary>
        public string Wechat { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public string Headcount { get; set; }
        /// <summary>
        /// 销售额
        /// </summary>
        public string Saleroom { get; set; }

        /// <summary>
        /// 活动记录
        /// </summary>
        public virtual ICollection<Activity> Activities { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public virtual ICollection<ClientContact> ClientContacts { get; set; }
    }
}
