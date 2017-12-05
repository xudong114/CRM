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
    /// GO佳居基础数据：账户信息
    /// </summary>
    [DisplayName("GO佳居基础数据：个人信息")]
    public class Base_AccountDTO : F_ModelRoot
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        [Required(ErrorMessage = "身份证号码为必填项")]
        public string IDNo { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        [DisplayName("开户行")]
        public string BankCode { get; set; }
        /// <summary>
        /// 虚拟账户
        /// </summary>
        [DisplayName("虚拟账户")]
        public string VirtualNo { get; set; }
        /// <summary>
        /// 关联账号
        /// </summary>
        [DisplayName("关联账号")]
        public string InlineNo { get; set; }
        /// <summary>
        /// 关联卡片照片
        /// </summary>
        [DisplayName("关联卡片照片")]
        public string CardImg { get; set; }
        /// <summary>
        /// 闪付二维码
        /// </summary>
        [DisplayName("闪付二维码")]
        public string QRCode { get; set; }
        /// <summary>
        /// 闪付识别码
        /// </summary>
        [DisplayName("闪付识别码")]
        public string PayCode { get; set; }
        /// <summary>
        /// 默认账号
        /// </summary>
        [DisplayName("默认账号")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
    }

    public class Base_AccountDTOList : List<Base_AccountDTO>
    {

    }

    public class Base_AccountDTOListWithPagination : PagedResult<Base_AccountDTO>
    {

    }

}
