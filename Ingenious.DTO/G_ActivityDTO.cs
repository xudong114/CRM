using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 消息
    /// </summary>
    [DisplayName("消息")]
    public class G_ActivityDTO : F_ModelRoot
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        [DisplayName("消息标题")]
        public string Title { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        [DisplayName("消息内容")]
        public string Content { get; set; }
        /// <summary>
        /// 已读
        /// </summary>
        [DisplayName("已读")]
        public bool IsRead { get; set; }
        /// <summary>
        /// 消息外部标识
        /// </summary>
        [DisplayName("消息外部标识")]
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 全局消息
        /// </summary>
        [DisplayName("全局消息")]
        public bool IsGlobal { get; set; }
        /// <summary>
        /// 创建时间格式化标签
        /// </summary>
        [DisplayName("创建时间格式化标签")]
        public string TimeLabel
        {
            get
            {
                return this.CreatedDate.ToString("yyyy-MM-dd");
            }
        }
    }

    public class G_ActivityDTOList : List<G_ActivityDTO> { }

    public class G_ActivityDTOListWithPagination : PagedResult<G_ActivityDTO>
    { }
}
