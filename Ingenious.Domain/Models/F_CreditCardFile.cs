
using System;
namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 信用卡申请-文件上传
    /// </summary>
    public class F_CreditCardFile : AggregateRoot
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 关联Id
        /// </summary>
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Path { get; set; }
    }
}
