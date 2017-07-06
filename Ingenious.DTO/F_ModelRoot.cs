using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Ingenious.DTO
{
    public class F_ModelRoot
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
        private F_UserDTO _CreatedByUser ;
        [IgnoreDataMember]
        public F_UserDTO CreatedByUser { get; set; }
        //{
        //    get
        //    {
        //        if (_CreatedByUser == null)
        //            return new F_UserDTO();
        //        return _CreatedByUser; 
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            _CreatedByUser = new F_UserDTO();
        //        }
        //        else
        //        {
        //            _CreatedByUser=value;
        //        }
        //    }
        //}
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
        private F_UserDTO _ModifiedByUser;
        [IgnoreDataMember]
        public F_UserDTO ModifiedByUser { get; set; }
        //{
        //     get {
        //         if (_ModifiedByUser == null)
        //             return new F_UserDTO();
        //         return _ModifiedByUser; 
        //    }
        //    set
        //    {
        //        if (value == null)
        //        {
        //            _ModifiedByUser = new F_UserDTO();
        //        }
        //        else
        //        {
        //            _ModifiedByUser = value;
        //        }
        //    }
        //}
        /// <summary>
        /// 数据状态
        /// </summary>
        [DisplayName("数据状态")]
        public bool IsActive { get; set; }
        /// <summary>
        /// 数据状态名称
        /// </summary>
        [IgnoreDataMember]
        public string IsActiveName
        {
            get
            {
                return this.IsActive ? "可用" : "已删除";
            }
        }
    }
}
