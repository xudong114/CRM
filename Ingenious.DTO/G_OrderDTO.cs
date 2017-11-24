using Ingenious.Domain.Models;
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
    public class G_OrderDTO : F_ModelRoot
    {
        public G_OrderDTO()
        {
            this.HasHouse = true;
        }
        /// <summary>
        /// 订单编号
        /// </summary>
        [DisplayName("订单编号")]
        public string Code { get; set; }

        /// <summary>
        /// 推广码
        /// </summary>
        [DisplayName("推广码")]
        public string ReferenceCode { get; set; }

        /// <summary>
        /// 贷款产品编码
        /// </summary>
        [DisplayName("贷款产品编码")]
        public string ProductCode { get; set; }

        /// <summary>
        /// 贷款产品名称
        /// </summary>
        [DisplayName("贷款产品名称")]
        public string ProductName { get; set; }

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
        /// 身份证正反面
        /// </summary>
        public string IDImg { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PersonalPhone { get; set; }

        #endregion

        #region 房产信息
        /// <summary>
        /// 是否有房产
        /// </summary>
        [DisplayName("是否有房产")]
        public bool HasHouse { get; set; }
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
        /// 房产所在地区（省 市 县）
        /// </summary>
        [DisplayName("房产所在地区（省 市 县）")]
        public string HouseAddress {
            get
            {
                return string.Format("{0} {1} {2}",this.Province,this.City,this.Country);
            }
        }

        /// <summary>
        /// 房产面积
        /// </summary>
        [DisplayName("房产面积")]
        public string Area { get; set; }
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

        #region Go佳居金融客服

        /// <summary>
        /// 金融客服工号
        /// </summary>
        [DisplayName("金融客服工号")]
        public string GoJiajuClerkCode { get; set; }

        /// <summary>
        /// 金融客服电话
        /// </summary>
        [DisplayName("金融客服电话")]
        public string GoJiajuClerkOfficePhone { get; set; }

        /// <summary>
        /// 金融客服姓名
        /// </summary>
        [DisplayName("金融客服姓名")]
        public string GoJiajuClerkName { get; set; }

        /// <summary>
        /// 金融经理工号
        /// </summary>
        [DisplayName("金融经理工号")]
        public string GoJiajuManagerCode { get; set; }
        /// <summary>
        /// 金融经理
        /// </summary>
        [DisplayName("金融经理")]
        public string GoJiajuManagerName { get; set; }
        /// <summary>
        /// 金融经理电话
        /// </summary>
        [DisplayName("金融经理电话")]
        public string GoJiajuManagerOfficePhone { get; set; }

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
        public G_OrderStatusEnum Status { get; set; }
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
        [DisplayName("订单审核记录")]
        public List<G_OrderRecordDTO> OrderRecordList { get; set; }

        /// <summary>
        /// 订单步骤记录
        /// </summary>
        [DisplayName("订单步骤记录")]
        public List<G_OrderRecordDTO> OrderStepRecordList { get; set; }

        /// <summary>
        /// 订单图片
        /// </summary>
        [DisplayName("订单图片")]
        public List<F_FileDTO> Files { get; set; }

        /// <summary>
        /// 经营工厂
        /// </summary>
        [DisplayName("经营工厂")]
        public Guid? Base_FactoryId { get; set; }
        /// <summary>
        /// 经营类型
        /// 1：工厂
        /// 0：店铺
        /// </summary>
        [DisplayName("经营类型")]
        public bool IsFactory { get; set; }
        /// <summary>
        /// 经营工厂
        /// </summary>
        [DisplayName("工厂")]
        public Base_FactoryDTO Base_Factory { get; set; }
        /// <summary>
        /// 店铺Id列表
        /// </summary>
        [DisplayName("店铺Id列表")]
        public string StoreIds { get; set; }

        /// <summary>
        /// 经营店铺
        /// </summary>
        [DisplayName("经营店铺")]
        public List<Base_StoreDTO> Stores { get; set; }

    }

    [DisplayName("贷款申请单List")]
    public class G_OrderDTOList : List<G_OrderDTO>
    { }

    [DisplayName("贷款申请单分页List")]
    public class G_OrderDTOListWithPagination : PagedResult<G_OrderDTO>
    {

    }

}
