using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 公司账号
    /// </summary>
    public class Account : AggregateRoot
    {
        /// <summary>
        /// 所属部门标识
        /// </summary>
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual Department Department { get; set; }
        /// <summary>
        /// 可用余额
        /// </summary>
        public decimal Balance { get; set; }

    }
}
