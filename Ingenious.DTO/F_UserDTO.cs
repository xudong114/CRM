using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 账号
    /// </summary>
    [DisplayName("账号")]
    public class F_UserDTO : F_ModelRoot
    {
        [DisplayName("账号")]
        public string UserName { get; set; }
        [DisplayName("密码")]
        public string Password { get; set; }
        [DisplayName("用户类型")]
        public F_UserTypeEnum UserType { get; set; }
        [DisplayName("用户详情")]
        public F_UserDetailDTO F_UserDetail { get; set; }

        /// <summary>
        /// 所属站点
        /// </summary>
        [DisplayName("所属站点")]
        public Guid WebsiteId { get; set; }
    }

    public class F_UserDTOList:List<F_UserDTO>
    { }
}
