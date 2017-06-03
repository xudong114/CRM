using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingenious.Infrastructure.Helper;

namespace Ingenious.DTO
{
    public class ContractDTO : ModelRoot
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        public string Code { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        [Required(ErrorMessage = "合同主题是必填项")]
        [DisplayName("主题")]
        public string Title { get; set; }
        /// <summary>
        /// 关联客户标识
        /// </summary>
        [Required(ErrorMessage = "关联客户是必填项")]
        [DisplayName("关联客户")]
        public Guid ClientId { get; set; }
        /// <summary>
        /// 关联客户
        /// </summary>
        public ClientDTO Client { get; set; }
        /// <summary>
        /// 总金额(元)
        /// </summary>
        [Required(ErrorMessage = "总金额(元)是必填项")]
        [DisplayName("总金额(元)")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        [Required(ErrorMessage = "开始日期是必填项")]
        [DisplayName("开始日期")]
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 开始日期(格式化：yyyy-MM-dd)
        /// </summary>
        public string BeginDateString { 
            get
            {
                return this.BeginDate.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// 结束日期
        /// </summary>
        [Required(ErrorMessage = "结束日期是必填项")]
        [DisplayName("结束日期")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 结束日期(格式化：yyyy-MM-dd)
        /// </summary>
        public string EndDateString
        {
            get
            {
                return this.EndDate.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 合同所有人标识
        /// </summary>
        [Required(ErrorMessage = "合同所有人是必填项")]
        [DisplayName("合同所有人")]
        public Guid OwnerId { get; set; }
        /// <summary>
        /// 合同所有人
        /// </summary>
        public UserDTO Owner { get; set; }
        /// <summary>
        /// 所属部门标识
        /// </summary>
        [Required(ErrorMessage = "所属部门是必填项")]
        [DisplayName("所属部门")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public DepartmentDTO Department { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        [DisplayName("合同状态")]
        public ContractStatusEnum Status { get; set; }
        /// <summary>
        /// 合同状态名称
        /// </summary>
        public string StatusLabel { 
            get
            {
                if (this.Status != 0)
                {
                    return this.Status.Discription();
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 付款方式
        /// </summary>
        [DisplayName("付款方式")]
        public PaymentMethodEnum Payment { get; set; }
        /// <summary>
        /// 付款方式名称
        /// </summary>
        public string PaymentLabel
        {
            get
            {
                if (this.Status != 0)
                {
                    return this.Payment.Discription();
                }
                return string.Empty;
            }
        }
        /// <summary>
        /// 产品标识
        /// </summary>
        [Required(ErrorMessage = "产品是必填项")]
        [DisplayName("产品")]
        public Guid ProductId { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public ProductDTO Product { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        public int Quantity { get; set; }
        /// <summary>
        /// 合同正文
        /// </summary>
        [DisplayName("合同正文")]
        public string Content { get; set; }
        /// <summary>
        /// 客户方签约人
        /// </summary>
        [DisplayName("客户方签约人")]
        public string ClientPrincipal { get; set; }
        /// <summary>
        /// 我方签约人
        /// </summary>
        [DisplayName("我方签约人")]
        public Guid? PrincipalId { get; set; }
        public UserDTO Principal { get; set; }
        /// <summary>
        /// 签约日期
        /// </summary>
        [DisplayName("签约日期")]
        public DateTime? ContractedDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Comment { get; set; }

    }

    public class ContractDTOList:List<ContractDTO>
    { }

}
