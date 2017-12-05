using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    public class Contract : AggregateRoot
    {
        /// <summary>
        /// 合同编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 关联客户
        /// </summary>
        public Guid ClientId { get; set; }
        public virtual Client Client { get; set; }
        /// <summary>
        /// 总金额(元)
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime BeginDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 合同所有人
        /// </summary>
        public Guid OwnerId { get; set; }
        //public virtual User Owner { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public Guid DepartmentId { get; set; }
        //public virtual Department Department { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        public ContractStatusEnum Status { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public PaymentMethodEnum Payment { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 合同正文
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 客户方签约人
        /// </summary>
        public string ClientPrincipal { get; set; }
        /// <summary>
        /// 我方签约人
        /// </summary>
        public Guid? PrincipalId { get; set; }
        //public virtual User Principal { get; set; }
        /// <summary>
        /// 签约日期
        /// </summary>
        public DateTime? ContractedDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }

    }
}
