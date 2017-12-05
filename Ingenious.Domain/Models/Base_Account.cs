using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// GO佳居基础数据：账户信息
    /// </summary>
    public class Base_Account : AggregateRoot
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 虚拟账户
        /// </summary>
        public string VirtualNo { get; set; }
        /// <summary>
        /// 关联账号
        /// </summary>
        public string InlineNo { get; set; }
        /// <summary>
        /// 关联卡片照片
        /// </summary>
        public string CardImg { get; set; }
        /// <summary>
        /// 闪付二维码
        /// </summary>
        public string QRCode { get; set; }
        /// <summary>
        /// 闪付识别码
        /// </summary>
        public string PayCode { get; set; }

        /// <summary>
        /// 默认账号
        /// </summary>
        public bool IsDefault { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }

}
