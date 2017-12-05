using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    public class G_Bank : AggregateRoot
    {
        /// <summary>
        /// 银行Logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 银行编码（唯一性）
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 上级银行
        /// </summary>
        public Guid? ParentId { get; set; }
        /// <summary>
        /// 是否总行
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 是否开启自动分配订单
        /// </summary>
        public bool IsAssignAuto { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }
    }
}
