using System;
using System.ComponentModel;

namespace Ingenious.DTO
{
    public class ModelRoot
    {
        [DisplayName("标识")]
        public Guid Id { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        [DisplayName("版本号")]
        public int Version { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DisplayName("创建人")]
        public Guid CreatedBy { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DisplayName("最后修改时间")]
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// 最后修改人
        /// </summary>
        [DisplayName("最后修改人")]
        public Guid ModifiedBy { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        [DisplayName("数据状态")]
        public bool IsActive { get; set; }
        /// <summary>
        /// 数据状态名称
        /// </summary>
        public string IsActiveName
        {
            get
            {
                return this.IsActive ? "可用" : "删除";
            }
        }
    }
}
