using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class F_BrankDTO : F_ModelRoot
    {
        /// <summary>
        /// 银行名称
        /// </summary>
        [DisplayName("银行名称")]
        public string Name { get; set; }
        /// <summary>
        /// 上级银行
        /// </summary>
        [DisplayName("上级银行")]
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 是否总行
        /// </summary>
        [DisplayName("是否总行")]
        public bool IsAdmin { get; set; }
    }
}
