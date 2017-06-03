using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 系统功能点
    /// </summary>
    public class Function : AggregateRoot
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Description { get; set; }
        public Guid ParentId { get; set; }
        public bool IsMenu { get; set; }
    }
}
