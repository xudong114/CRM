using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ingenious.DTO
{
    /// <summary>
    /// 部门（包括公司和经销商都作为部门来处理，可设置多级部门）
    /// </summary>
    public class DepartmentDTO : ModelRoot
    {
        /// <summary>
        /// 分支机构
        /// </summary>
        [DisplayName("分支机构")]
        public bool IsBranch { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [Required(ErrorMessage = "部门名称是必填项")]
        [DisplayName("部门名称")]
        public string Name { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        [DisplayName("所属部门")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public DepartmentDTO Parent { get; set; }

        /// <summary>
        /// 部门负责人
        /// </summary>
        [DisplayName("联系人")]
        public string Principal { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string Phone { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        [DisplayName("办公电话")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 办公地址
        /// </summary>
        [DisplayName("办公地址")]
        public string Address { get; set; }

        /// <summary>
        /// Go佳居站点
        /// </summary>
        [DisplayName("Go佳居站点")]
        public string Website { get; set; }

        /// <summary>
        /// 系统用户
        /// </summary>
        public List<UserDTO> Users { get; set; }
        /// <summary>
        /// Go佳居站点
        /// </summary>
        public DepartmentDTOList Children { get; set; }
        /// <summary>
        /// 价格策略标识
        /// </summary>
        [DisplayName("价格策略")]
        [Required(ErrorMessage = "价格策略是必填项")]
        public Guid? PriceStrategyId { get; set; }
        /// <summary>
        /// 价格策略
        /// </summary>
        public PriceStrategyDTO PriceStrategy { get; set; }
        public string PriceStrategyName
        {
            get
            {
                return this.PriceStrategy == null ? "" : this.PriceStrategy.Name;
            }
        }

    }

    public class DepartmentDTOList : List<DepartmentDTO> { }
}
