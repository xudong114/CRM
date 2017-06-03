using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 活动记录类型
    /// </summary>
    public class ActivityCategory : AggregateRoot
    {
        public string Name { get; set; }
        public string Desc { get; set; }
    }
}
