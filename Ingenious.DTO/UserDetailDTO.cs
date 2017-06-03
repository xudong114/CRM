using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class UserDetailDTO : ModelRoot
    {
        /// <summary>
        /// 头像路径
        /// </summary>
        [DisplayName("头像")]
        public string Logo { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Required]
        [DisplayName("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public bool Gender { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        public string Title { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [DisplayName("出生日期")]
        public DateTime? Birthdate { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        [DisplayName("办公电话")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        [DisplayName("微信")]
        public string Wechat { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        [DisplayName("QQ")] 
        public string QQ { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        [DisplayName("入职日期")] 
        public DateTime? Hiredate { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        [DisplayName("离职日期")]
        public DateTime? Termdate { get; set; }

    }
}
