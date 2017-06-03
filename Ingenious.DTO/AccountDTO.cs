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
    /// 公司账号
    /// </summary>
    public class AccountDTO : ModelRoot
    {
        /// <summary>
        /// 所属部门标识
        /// </summary>
        [DisplayName("所属部门")]
        [Required(ErrorMessage = "所属部门为必填项")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public DepartmentDTO Department { get; set; }
        /// <summary>
        /// 可用余额
        /// </summary>
        [DisplayName("可用余额")]
        public decimal Balance { get; set; }

    }

    public class AccountDTOList:List<AccountDTO>
    { }
}
