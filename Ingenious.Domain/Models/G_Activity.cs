using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 消息
    /// </summary>
    public class G_Activity : AggregateRoot
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 已读
        /// </summary>
        public bool IsRead { get; set; }
        /// <summary>
        /// 消息外部标识
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 全局消息
        /// </summary>
        public bool IsGlobal { get; set; }
    }
}
