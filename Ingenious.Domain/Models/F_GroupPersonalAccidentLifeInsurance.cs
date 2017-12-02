using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 团体人身伤害意外保险
    /// </summary>
    [DisplayName("团体人身伤害意外保险")]
    public class F_GroupPersonalAccidentLifeInsurance : AggregateRoot
    {
        /// <summary>
        /// 组织名称
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string BusinessLicenseImg { get; set; }
        /// <summary>
        /// 企业法人名称
        /// </summary>
        public string LegalPersonName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 身份证（正反面）
        /// </summary>
        public string IDImg { get; set; }
    }
}
