using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    public class F_Brank : AggregateRoot
    {
        /// <summary>
        /// 银行名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 上级银行
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 是否总行
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
