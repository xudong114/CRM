
using Ingenious.Infrastructure.Enum;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 系统账号
    /// </summary>
    public class User : AggregateRoot
    {
        /// <summary>
        /// 关联用户标识
        /// </summary>
        public Guid UserDetailId { get; set; }
        /// <summary>
        /// 关联用户信息
        /// </summary>
        public virtual UserDetail UserDetail { get; set; }
        /// <summary>
        /// 所属部门标识
        /// </summary>
        public Guid? DepartmentId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual Department Department { get; set; }
        /// <summary>
        /// 所属机构
        /// </summary>
        public Guid BranchId { get; set; }
        public virtual Department Branch { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSupper { get; set; }
        public UserStatusEnum Status { get; set; }

    }
}
