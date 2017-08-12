using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 文件管理
    /// </summary>
    public class F_File : AggregateRoot
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件编码
        /// </summary>
        public string  Code { get; set; }
        /// <summary>
        /// 关联标识（Id）
        /// </summary>
        public Guid  ReferenceId { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string  Path { get; set; }

    }
}
