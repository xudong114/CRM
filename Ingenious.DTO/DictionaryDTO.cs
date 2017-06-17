using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class DictionaryDTO : ModelRoot
    {
       
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [DisplayName("编码")]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Sort { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        [DisplayName("分类")]
        public string Category { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Description { get; set; }
    }

    public class DictionaryDTOList : List<DictionaryDTO>{}
}
