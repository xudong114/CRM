using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 信用卡申请-文件上传
    /// </summary>
    public class F_CreditCardFileDTO : F_ModelRoot
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        [DisplayName("文件类型")]
        public string Category { get; set; }
        /// <summary>
        /// 关联Id
        /// </summary>
        [DisplayName("关联Id")]
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [DisplayName("文件路径")]
        public string Path { get; set; }
    }
}
