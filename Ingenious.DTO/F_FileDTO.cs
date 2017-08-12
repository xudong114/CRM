using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [DisplayName("文件管理")]
    public class F_FileDTO : F_ModelRoot
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        [DisplayName("文件名称")]
        public string Name { get; set; }
        /// <summary>
        /// 文件编码
        /// </summary>
        [DisplayName("文件编码")]
        public string Code { get; set; }
        /// <summary>
        /// 关联标识（Id）
        /// </summary>
        [DisplayName("关联标识（Id）")]
        public Guid ReferenceId { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        [DisplayName("文件路径")]
        public string Path { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>
        [DisplayName("文件流")]
        public Stream Data { get; set; }
    }

    public class F_FileDTOList:List<F_FileDTO>
    { }

}
