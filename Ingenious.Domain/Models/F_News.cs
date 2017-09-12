using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 新闻
    /// </summary>
    public class F_News : AggregateRoot
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 形象图
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 唯一数字编码
        /// </summary>
        public int Key { get; set; }
    }
}
