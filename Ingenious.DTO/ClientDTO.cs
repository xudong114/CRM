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
    /// 客户
    /// </summary>
    public class ClientDTO : ModelRoot
    {
        /// <summary>
        /// 客户名称
        /// </summary>
        [Required(ErrorMessage = "客户名称是必填项")]
        [DisplayName("客户名称")]
        public string Name { get; set; }
        /// <summary>
        /// 所属账号
        /// </summary>
        [DisplayName("所属账号")]
        public Guid? UserId { get; set; }
        public virtual UserDTO User { get; set; }
        public string UserName
        {
            get
            {
                return this.User == null ? "" : this.User.UserName;
            }
        }
        /// <summary>
        /// 省份
        /// </summary>
        [DisplayName("省份")]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [DisplayName("城市")]
        public string City { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        [DisplayName("县区")]
        public string Country { get; set; }
        /// <summary>
        /// 省 市 区
        /// </summary>
        public string District
        {
            get
            {
                return string.Format("{0} {1} {2}", this.Province, this.City , this.Country);
            }
        }
        /// <summary>
        /// 省 市 区 街道
        /// </summary>
        public string Address
        {
            get
            {
                return string.Format("{0} {1}", this.District, this.Street);
            }
        }

        /// <summary>
        /// 街道
        /// </summary>
        [DisplayName("街道")]
        public string Street { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Comment { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [Required(ErrorMessage = "所属部门是必填项")]
        [DisplayName("所属部门")]
        public Guid DepartmentId { get; set; }
        public DepartmentDTO Department { get; set; }
        /// <summary>
        /// 部门全称(一级名称 二级名称)
        /// </summary>
        public string DepartmentFullName
        {
            get
            {
                if (!this.Department.ParentId.HasValue)
                {
                    return this.Department.Name;
                }

                var name = new StringBuilder();
                var dept = this.Department;

                while (dept.ParentId.HasValue)
                {
                    name.Insert(0, string.Format("{1}{0}", dept.Name, dept.Parent.ParentId.HasValue ? "-" : ""));
                    dept = dept.Parent;
                }
                return name.ToString();
            }
        }
        /// <summary>
        /// 等级
        /// </summary>
        [DisplayName("等级")]
        public Guid? GradeId { get; set; }
        public GradeDTO Grade { get; set; }
        public string GradeName
        {
            get
            {
                return this.Grade == null ? "" : this.Grade.Name;
            }
        }
        /// <summary>
        /// 行业
        /// </summary>
        [DisplayName("行业")]
        public Guid? IndustryId { get; set; }
        public IndustryDTO Industry { get; set; }
        public string IndustryName
        {
            get
            {
                return this.Industry == null ? "" : this.Industry.Name;
            }
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        public string Postcode { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        [DisplayName("传真")]
        public string Fax { get; set; }
        /// <summary>
        /// 公司网址
        /// </summary>
        [DisplayName("公司网址")]
        public string Website { get; set; }
        /// <summary>
        /// 微博
        /// </summary>
        [DisplayName("微博")]
        public string Weibo { get; set; }
        /// <summary>
        /// 微信公众号
        /// </summary>
        [DisplayName("微信公众号")]
        public string Wechat { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        [DisplayName("总人数")]
        public string Headcount { get; set; }
        /// <summary>
        /// 销售额
        /// </summary>
        [DisplayName("销售额")]
        public string Saleroom { get; set; }
        /// <summary>
        /// 合同列表
        /// </summary>
        public ContractDTOList ContractDTOList { get; set; }
    }

    public class ClientDTOList : List<ClientDTO>
    { }
}
