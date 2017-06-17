using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenious.Infrastructure.Helper;

namespace Ingenious.DTO
{
    /// <summary>
    /// 系统账号
    /// </summary>
    public class UserDTO : ModelRoot
    {
        //public UserDTO()
        //{
        //    this.UserDetailId = Guid.Empty;
        //    this.UserDetail = new UserDetailDTO { Name = "-" };
        //    this.DepartmentId = Guid.Empty;
        //    this.Department = new DepartmentDTO();
        //    this.BranchId = Guid.Empty;
        //    this.Branch = new DepartmentDTO();
        //    this.UserName = "-";
        //    this.Password = string.Empty;
        //    this.IsAdmin = false;
        //    this.IsSupper = false;
        //    this.Status = UserStatusEnum.All;
        //}

        /// <summary>
        /// 账户名称
        /// </summary>
        [DisplayName("账号")]
        [Required(ErrorMessage = "账号是必填项")]
        public string UserName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name {

            get {
                return this.UserDetail == null ? this.UserName : this.UserDetail.Name;
            }
        }

        /// <summary>
        /// 账户密码
        /// </summary>
        [DisplayName("密码")]
        [Required(ErrorMessage = "密码是必填项")]
        public string Password { get; set; }

        /// <summary>
        /// 账户状态
        /// </summary>
        [DisplayName("状态")]
        public UserStatusEnum Status { get; set; }
        /// <summary>
        /// 账户状态描述
        /// </summary>
        public string StatusName
        {
            get
            {
                if(this.Status!=0)
                {
                   return this.Status.Discription() ;
                }
                return string.Empty;
            }
        }

        /// <summary>
        /// 部门管理员
        /// </summary>
        [DisplayName("部门管理员")]
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 系统管理员
        /// </summary>
        [DisplayName("系统管理员")]
        public bool IsSupper { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [DisplayName("所属部门")]
        public Guid? DepartmentId { get; set; }
        public DepartmentDTO Department { get; set; }

        /// <summary>
        /// 所属机构
        /// </summary>
        [DisplayName("所属机构")]
        public Guid BranchId { get; set; }
        public DepartmentDTO Branch { get; set; }

        public Guid? UserDetailId { get; set; }
        public UserDetailDTO UserDetail { get; set; }

        /// <summary>
        /// 部门标签
        /// </summary>
        public string DepartmentName
        {
            get
            {
                return string.Format("{0}-{1}", this.Branch.Name, this.Department == null ? "" : this.Department.Name);
            }
        }

    }

    public class UserDTOList : List<UserDTO>
    { }


}
