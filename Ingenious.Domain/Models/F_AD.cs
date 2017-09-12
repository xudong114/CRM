using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 广告
    /// </summary>
    public class F_AD : AggregateRoot
    {
        /// <summary>
        /// 广告名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 广告链接
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 广告图片
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginDate { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 广告分类
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
