using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 店铺信息
    /// </summary>
    public class F_Store : AggregateRoot
    {
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 店铺编码
        /// </summary>
        public string  Code { get; set; }
        /// <summary>
        /// 所属站点
        /// </summary>
        public Guid WebsiteId { get; set; }
        /// <summary>
        /// 店铺英文名称
        /// </summary>
        public string EnglishName { get; set; }
        /// <summary>
        /// 店铺logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
