using Ingenious.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// GO佳居基础数据：个人信息
    /// </summary>
    [DisplayName("GO佳居基础数据：个人信息")]
    public class Base_ProfileDTO : F_ModelRoot
    {
        /// <summary>
        /// 客户编码
        /// </summary>
        [DisplayName("客户编码")]
        public string Code { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名为必填项")]
        public string Name { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        [DisplayName("籍贯")]
        public string NativePlace { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string Gender { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        [Required(ErrorMessage = "身份证号码为必填项")]
        public string IDNo { get; set; }
        /// <summary>
        /// 身份证照片
        /// 格式：正面|反面|手持身份证，使用“|”分割。
        /// 增加手持身份证照片
        /// </summary>
        [DisplayName("身份证照片")]
        [Required(ErrorMessage = "身份证照片为必填项")]
        public string IDImg { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        [Required(ErrorMessage = "手机号码为必填项")]
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 办公电话
        /// </summary>
        [DisplayName("办公电话")]
        public string OfficePhone { get; set; }
        /// <summary>
        /// 微信号
        /// </summary>
        [DisplayName("微信号")]
        public string Wechat { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DisplayName("电子邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// QQ账号
        /// </summary>
        [DisplayName("QQ")]
        public string QQ { get; set; }
        /// <summary>
        /// 微博账号
        /// </summary>
        [DisplayName("微博账号")]
        public string Weibo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
         [DisplayName("备注")]
        public string Remark { get; set; }
    }

    public class Base_ProfileDTOList : List<Base_ProfileDTO>
    {

    }

    public class Base_ProfileDTOListWithPagination : PagedResult<Base_ProfileDTO>
    {

    }

}
