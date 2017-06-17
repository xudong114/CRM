using Ingenious.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain
{
    public class AggregateRoot : IAggregateRoot
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreatedBy { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public Guid ModifiedBy { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public bool IsActive { get; set; }
    }
}
