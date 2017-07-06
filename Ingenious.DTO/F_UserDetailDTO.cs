using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class F_UserDetailDTO : F_ModelRoot
    {
        /// <summary>
        /// 关联账户
        /// </summary>
        public Guid F_UserId { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        public string Face { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 所在银行
        /// </summary>
        public Guid? BrandId { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthdate { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 微信
        /// </summary>
        public string Wechat { get; set; }
        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }
    }
}
