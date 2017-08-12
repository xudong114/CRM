
using System;
namespace Ingenious.Domain.Models
{
    public class F_UserDetail : AggregateRoot
    {
        /// <summary>
        /// 关联账户
        /// </summary>
        public Guid F_UserId { get; set; }

        public virtual F_User F_User { get; set; }

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
        public string BankCode { get; set; }
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
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string  City { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        public string  Country { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        public string  Street { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string Code { get; set; }
    }
}
