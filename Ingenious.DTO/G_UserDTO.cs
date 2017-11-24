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
    public class G_UserDTO : F_ModelRoot
    {
        [DisplayName("账号")]
        public string UserName { get; set; }
        [DisplayName("密码")]
        public string Password { get; set; }
        [DisplayName("用户类型")]
        public G_UserTypeEnum UserType { get; set; }
        [DisplayName("用户详情")]
        public G_UserDetailDTO G_UserDetail { get; set; }

        /// <summary>
        /// 所属站点
        /// </summary>
        [DisplayName("所属站点")]
        public Guid WebsiteId { get; set; }

        /// <summary>
        /// 机构编号
        /// </summary>
         [DisplayName("机构编号")]
        public Guid? G_EntityId { get; set; }
        /// <summary>
        /// 所属机构
        /// </summary>
        public G_EntityDTO G_Entity { get; set; }

        /// <summary>
        /// 是否是机构管理员
        /// </summary>
        [DisplayName("是否是机构管理员")]
        public bool IsAdmin
        {
            get
            {
                if (G_Entity != null
                    && G_Entity.UserId != null
                    && G_Entity.UserId == this.G_Entity.UserId)
                {
                    return true;
                }
                return false;
            }
        }

    }

    public class G_UserDTOList:List<G_UserDTO>
    { }
}
