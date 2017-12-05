using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 团体人身伤害意外保险员工信息
    /// </summary>
    [DisplayName("团体人身伤害意外保险员工信息")]
    public class F_GroupPersonalAccidentLifeInsuranceEmployee : F_ModelRoot
    {
        /// <summary>
        /// 所属公司
        /// </summary>
        [DisplayName("所属公司")]
        public Guid F_GroupPersonalAccidentLifeInsuranceId { get; set; }
        /// <summary>
        /// 所属公司
        /// </summary>
        [DisplayName("所属公司")]
        public virtual F_GroupPersonalAccidentLifeInsurance F_GroupPersonalAccidentLifeInsurance { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string Gender { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        public string Title { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// 工种
        /// </summary>
        public string TypeOfWork { get; set; }
        /// <summary>
        /// 身份证（正反面）
        /// </summary>
        public string IDImg { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PersonalPhone { get; set; }
    }
}
