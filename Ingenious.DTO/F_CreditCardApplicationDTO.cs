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
    /// 信用卡申请资料
    /// </summary>
    [DisplayName("信用卡申请资料")]
    public class F_CreditCardApplicationDTO : F_ModelRoot
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        [Required(ErrorMessage = "姓名必填")]
        public string Name { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [DisplayName("身份证号")]
        public string IDNO { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        [Required(ErrorMessage = "联系电话必填")]
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        [DisplayName("籍贯")]
        public string NativePlace { get; set; }
        /// <summary>
        /// 所属行业（软体、实木、其他）
        /// </summary>
        [DisplayName("所属行业")]
        public string Industry { get; set; }
        /// <summary>
        /// 经营性质（工厂、专卖店）
        /// </summary>
        [DisplayName("经营性质")]
        public string Category { get; set; }
        /// <summary>
        /// 数量（工厂、专卖店）
        /// </summary>
        [DisplayName("数量")]
        public string Quantity { get; set; }
        /// <summary>
        /// 工厂/专卖店地址
        /// </summary>
        [DisplayName("工厂/专卖店地址")]
        public string StoreAddress { get; set; }
        /// <summary>
        /// 信用卡信息
        /// </summary>
        [DisplayName("信用卡信息")]
        public string CreditCard { get; set; }
        /// <summary>
        /// 逾期信息
        /// </summary>
        [DisplayName("逾期信息")]
        public string Pastdue { get; set; }
        /// <summary>
        /// 使用额度比
        /// </summary>
        [DisplayName("使用额度比")]
        public string UsedRate { get; set; }
        /// <summary>
        /// 房产信息
        /// </summary>
        [DisplayName("房产信息")]
        public string House { get; set; }
        /// <summary>
        /// 车辆信息
        /// </summary>
        [DisplayName("车辆信息")]
        public string Car { get; set; }
        /// <summary>
        /// 是否有贷款
        /// </summary>
        [DisplayName("是否有贷款")]
        public bool HasLoan { get; set; }
        /// <summary>
        /// 贷款金额
        /// </summary>
        [DisplayName("贷款金额")]
        public decimal LoanAmount { get; set; }

        /// <summary>
        /// 社会保险号
        /// </summary>
        [DisplayName("社会保险号")]
        public string SIN { get; set; }
        /// <summary>
        /// 身份证照片
        /// </summary>
        [DisplayName("身份证照片")]
        public string IDNOImage { get; set; }
        /// <summary>
        /// 结婚证照片
        /// </summary>
        [DisplayName("结婚证照片")]
        public string MrriageImage { get; set; }
        /// <summary>
        /// 户口本照片
        /// </summary>
        [DisplayName("户口本照片")]
        public string HouseholdRegisterImage { get; set; }
        /// <summary>
        /// 房产证照片
        /// </summary>
        [DisplayName("房产证照片")]
        public string HouseholdImage { get; set; }
        /// <summary>
        /// 驾照照片
        /// </summary>
        [DisplayName("行驶证")]
        public string CarholdRegisterImage { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        [DisplayName("营业执照")]
        public string BusinessLicense { get; set; }
    }
}
