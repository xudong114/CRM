using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 个人详情
    /// </summary>
    [DisplayName("个人详情")]
    public class F_UserDetailDTO : F_ModelRoot
    {
        /// <summary>
        /// 关联账户
        /// </summary>
        [DisplayName("关联账户")]
        public Guid F_UserId { get; set; }

        public F_UserDTO F_User { get; set; }
        /// <summary>
        /// 头像路径
        /// </summary>
        [DisplayName("头像路径")]
        public string Face { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [DisplayName("昵称")]
        public string NickName { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public bool Gender { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 所在银行（F_Bank.Code）
        /// </summary>
        [DisplayName("所在银行（F_Bank.Code）")]
        public string BankCode { get; set; }
        /// <summary>
        /// 所在银行（F_Bank.Name）
        /// </summary>
        [DisplayName("所在银行（F_Bank.Name）")]
        public string BankName { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        public string Title { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        [DisplayName("办公电话")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [DisplayName("出生日期")]
        public DateTime? Birthdate { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PersonalPhone { get; set; }
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
        /// 省
        /// </summary>
        [DisplayName("省")]
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        [DisplayName("城市")]
        public string City { get; set; }
        /// <summary>
        /// 县区
        /// </summary>
        [DisplayName("县区")]
        public string Country { get; set; }
        /// <summary>
        /// 街道
        /// </summary>
        [DisplayName("街道")]
        public string Street { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        public string IDNo { get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        [DisplayName("工号")]
        public string Code { get; set; }
    }
}
