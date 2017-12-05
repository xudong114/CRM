
using System;
namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 信用卡申请资料
    /// </summary>
    public class F_CreditCardApplication : AggregateRoot
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IDNO { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string PersonalPhone { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace{get;set;}
        /// <summary>
        /// 所属行业（软体、实木、其他）
        /// </summary>
        public string Industry { get; set; }
        /// <summary>
        /// 经营性质（工厂、专卖店）
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 数量（工厂、专卖店）
        /// </summary>
        public string Quantity { get; set; }
        /// <summary>
        /// 工厂/专卖店地址
        /// </summary>
        public string StoreAddress { get; set; }
        /// <summary>
        /// 信用卡信息
        /// </summary>
        public string CreditCard { get; set; }
        /// <summary>
        /// 逾期信息
        /// </summary>
        public string Pastdue { get; set; }
        /// <summary>
        /// 使用额度比
        /// </summary>
        public string UsedRate { get; set; }
        /// <summary>
        /// 房产信息
        /// </summary>
        public string House { get; set; }
        /// <summary>
        /// 车辆信息
        /// </summary>
        public string Car { get; set; }
        /// <summary>
        /// 是否有贷款
        /// </summary>
        public bool HasLoan { get; set; }
        /// <summary>
        /// 贷款金额
        /// </summary>
        public decimal LoanAmount { get; set; }
        /// <summary>
        /// 社会保险号
        /// </summary>
        public string SIN {get;set;}
        /// <summary>
        /// 身份证照片
        /// </summary>
        public string IDNOImage{get;set;}
        /// <summary>
        /// 结婚证照片
        /// </summary>
        public string MrriageImage{get;set;}
        /// <summary>
        /// 户口本照片
        /// </summary>
        public string HouseholdRegisterImage{get;set;}
        /// <summary>
        /// 房产证照片
        /// </summary>
        public string HouseholdImage{get;set;}
        /// <summary>
        /// 驾照照片
        /// </summary>
        public string CarholdRegisterImage{get;set;}
        /// <summary>
        /// 营业执照
        /// </summary>
        public string BusinessLicense { get; set; }
    }
}
