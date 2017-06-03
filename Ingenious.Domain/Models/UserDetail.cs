
using System;
namespace Ingenious.Domain.Models
{
    public class UserDetail : AggregateRoot
    {
        /// <summary>
        /// 头像路径
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthdate { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string Wechat { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public DateTime? Hiredate { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        public DateTime? Termdate { get; set; }

    }
}
