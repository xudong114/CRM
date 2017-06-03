using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 活动记录
    /// </summary>
    public class Activity : AggregateRoot
    {
        /// <summary>
        /// 关联记录目标标识
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 记录内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public Guid ActivityCategoryId { get; set; }
        /// <summary>
        /// 记录类型
        /// </summary>
        public virtual ActivityCategory ActivityCategory { get; set; }
        /// <summary>
        /// 评论主体标识
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 评论主体
        /// </summary>
        public Activity Parent { get; set; }
        /// <summary>
        /// 评论人标识
        /// </summary>
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }

}
