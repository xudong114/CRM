using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 部门（包括公司和经销商都作为部门来处理，可设置多级部门）
    /// </summary>
    public class Department : AggregateRoot
    {
        /// <summary>
        /// 子公司
        /// </summary>
        public bool IsBranch { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public Guid? ParentId { get; set; }
        public virtual Department Parent { get; set; }
        /// <summary>
        /// 部门负责人
        /// </summary>
        public string Principal { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// 办公地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// Go佳居站点
        /// </summary>
        public string Website { get; set; }
        /// <summary>
        /// 系统用户
        /// </summary>
        public virtual ICollection<User> Users { get; set; }
        /// <summary>
        /// 下级部门集合
        /// </summary>
        public virtual ICollection<Department> Children { get; set; }
        /// <summary>
        /// 价格策略标识
        /// </summary>
        public Guid? PriceStrategyId { get; set; }
        /// <summary>
        /// 价格策略
        /// </summary>
        public virtual PriceStrategy PriceStrategy { get; set; }
    }
}
