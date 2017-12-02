using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Enum;
using Ingenious.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 贷款申请单
    /// </summary>
    [DisplayName("贷款申请单")]
    public class F_OrderDTO : F_ModelRoot
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [DisplayName("订单编号")]
        public string Code { get; set; }
        #region 个人信息
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string Name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [DisplayName("身份证号码")]
        public string IDNo { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PersonalPhone { get; set; }

        #endregion

        #region 房产信息
        /// <summary>
        /// 房产所在省份
        /// </summary>
        [DisplayName("房产所在省份")]
        public string Province { get; set; }
        /// <summary>
        /// 房产所在城市
        /// </summary>
        [DisplayName("房产所在城市")]
        public string City { get; set; }
        /// <summary>
        /// 房产所在县区
        /// </summary>
        [DisplayName("房产所在县区")]
        public string Country { get; set; }
        /// <summary>
        /// 房产所在小区
        /// </summary>
        [DisplayName("房产所在小区")]
        public string Community { get; set; }
        /// <summary>
        /// 房产户主，与房产证上户主姓名一致
        /// </summary>
        [DisplayName("房产户主，与房产证上户主姓名一致")]
        public string Landlord { get; set; }
        /// <summary>
        /// 是否分期付款
        /// </summary>
        [DisplayName("是否分期付款")]
        public bool IsInstallment { get; set; }

        /// <summary>
        /// 贷款银行标识（F_Bank.Code）,非必填项，根据是否分期来决定。
        /// </summary>
        [DisplayName("贷款银行标识（F_Bank.Code）,非必填项，根据是否分期来决定。")]
        public string FromBank { get; set; }

        #endregion

        #region 交易信息
        /// <summary>
        /// 店铺编码
        /// </summary>
        [DisplayName("店铺编码")]
        public string StoreCode { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        [DisplayName("店铺名称")]
        public string StoreName { get; set; }

        /// <summary>
        /// 导购工号
        /// </summary>
        [DisplayName("导购工号")]
        public string ClerkCode { get; set; }

        /// <summary>
        /// 导购姓名
        /// </summary>
        [DisplayName("导购姓名")]
        public string ClerkName { get; set; }

        /// <summary>
        /// 客服工号
        /// </summary>
        [DisplayName("客服工号")]
        public string GoJiajuClerkCode { get; set; }

        /// <summary>
        /// 客服电话
        /// </summary>
        [DisplayName("客服电话")]
        public string GoJiajuClerkOfficePhone { get; set; }

        /// <summary>
        /// 客服姓名
        /// </summary>
        [DisplayName("客服姓名")]
        public string GoJiajuClerkName { get; set; }

        /// <summary>
        /// 交易总额（万元）
        /// </summary>
        [DisplayName("交易总额（万元）")]
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 首付金额
        /// </summary>
        [DisplayName("首付金额")]
        public decimal DownpaymentAmount { get; set; }
        /// <summary>
        /// 申请贷款金额
        /// </summary>
        [DisplayName("申请贷款金额")]
        public decimal LoanAmount { get; set; }
        /// <summary>
        /// 实际发放金额
        /// </summary>
        [DisplayName("实际发放金额")]
        public decimal GotLoanAmount { get; set; }
        #endregion

        #region 银行审批
        /// <summary>
        /// 审批银行（F_Bank.Code）
        /// </summary>
        [DisplayName("审批银行（F_Bank.Code）")]
        public string BankCode { get; set; }

        /// <summary>
        /// 审批银行（F_Bank.Name）
        /// </summary>
        [DisplayName("审批银行（F_Bank.Name）")]
        public string BankName { get; set; }

        /// <summary>
        /// 信贷经理（F_UserDetail.Code）
        /// </summary>
        [DisplayName("信贷经理（F_UserDetail.Code）")]
        public string BankManager { get; set; }
        /// <summary>
        /// 信贷经理（F_UserDetail.Name）
        /// </summary>
        [DisplayName("信贷经理姓名（F_UserDetail.Name）")]
        public string BankManagerName { get; set; }
        /// <summary>
        /// 客户经理（F_UserDetail.Code）
        /// </summary>
        [DisplayName("客户经理（F_UserDetail.Code）")]
        public string BankClerk { get; set; }
        /// <summary>
        /// 客户经理姓名（F_UserDetail.Name）
        /// </summary>
        [DisplayName("客户经理姓名（F_UserDetail.Name）")]
        public string BankClerkName { get; set; }

        #endregion

        /// <summary>
        /// 订单状态
        /// </summary>
        [DisplayName("订单状态")]
        public F_OrderStatusEnum Status { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [DisplayName("订单状态")]
        public string StatusName
        {
            get
            {
                return this.Status.Discription();
            }
        }
        /// <summary>
        /// 订单创建时间格式化标签（yyyy.MM.dd）
        /// </summary>
        [DisplayName("订单创建时间标签")]
        public string CreatedDateLabel
        {
            get
            {
                return this.CreatedDate.ToString("yyyy.MM.dd");
            }
        }

        /// <summary>
        /// 订单审核记录
        /// </summary>
        public List<F_OrderRecordDTO> OrderRecordList { get; set; }
        /// <summary>
        /// 订单图片
        /// </summary>
        public List<F_FileDTO> Files { get; set; }
    }

    [DisplayName("贷款申请单List")]
    public class F_OrderDTOList : List<F_OrderDTO>
    { }

    [DisplayName("贷款申请单分页List")]
    public class F_OrderDTOListWithPagination : PagedResult<F_OrderDTO>
    {

    }
}
