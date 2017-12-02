using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class Base_BankDTO : F_ModelRoot
    {
        /// <summary>
        /// 银行Logo
        /// </summary>
        [DisplayName("Logo")]
        public string Logo { get; set; }
        /// <summary>
        /// 银行名称
        /// </summary>
        [DisplayName("银行名称")]
        [Required(ErrorMessage="银行名称必填")]
        public string Name { get; set; }
        /// <summary>
        /// 银行编码（唯一性）
        /// </summary>
        [DisplayName("银行编码")]
        [Required(ErrorMessage = "银行编码必填")]
        public string Code { get; set; }
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
        /// <summary>
        /// 是否开启自动分配订单
        /// </summary>
        [DisplayName("是否开启自动分配订单")]
        public bool IsAssignAuto { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        public int Order { get; set; }
    }
}
