using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 金融用户账号
    /// </summary>
    public class G_User : AggregateRoot
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public G_UserTypeEnum UserType { get; set; }
        /// <summary>
        /// 所属站点
        /// </summary>
        public Guid WebsiteId { get; set; }
        /// <summary>
        /// 机构编号
        /// </summary>
        public Guid? G_EntityId { get; set; }
        /// <summary>
        /// 所属机构
        /// </summary>
        public virtual G_Entity G_Entity { get; set; }
    }
}
