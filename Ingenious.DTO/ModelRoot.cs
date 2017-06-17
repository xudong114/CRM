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
        private UserDTO _CreatedByUser ;
        public UserDTO CreatedByUser
        {
            get
            {
                if (_CreatedByUser == null)
                    return new UserDTO();
                return _CreatedByUser; 
            }
            set
            {
                if (value == null)
                {
                    _CreatedByUser = new UserDTO();
                }
                else
                {
                    _CreatedByUser=value;
                }
            }
        }
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
        private UserDTO _ModifiedByUser ;
        public UserDTO ModifiedByUser
        {
             get {
                 if (_ModifiedByUser == null)
                     return new UserDTO();
                 return _ModifiedByUser; 
            }
            set
            {
                if (value == null)
                {
                    _ModifiedByUser = new UserDTO();
                }
                else
                {
                    _ModifiedByUser = value;
                }
            }
        }
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
                return this.IsActive ? "可用" : "已删除";
            }
        }
    }
}
