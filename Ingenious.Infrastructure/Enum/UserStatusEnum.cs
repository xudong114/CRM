
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Ingenious.Infrastructure.Enum
{
    public enum UserStatusEnum
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        All = 0,

        /// <summary>
        /// 在职
        /// </summary>
        [Description("在职")]
        Available = 2,
        /// <summary>
        /// 禁用
        /// </summary>
        [Description("禁用")]
        Disabled = 4,
        /// <summary>
        /// 离职
        /// </summary>
        [Description("离职")]
        Departured = 8,
        /// <summary>
        /// 锁住
        /// </summary>
        [Description("锁住")]
        Locked = 16
    }
}
