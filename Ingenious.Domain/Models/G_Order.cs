using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 贷款申请单
    /// </summary>
    public class G_Order : AggregateRoot
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 推广码
        /// </summary>
        public string ReferenceCode { get; set; }

        /// <summary>
        /// 贷款产品编码
        /// </summary>
        public string ProductCode { get; set; }

        /// <summary>
        /// 经营类型
        /// 1：工厂
        /// 0：店铺
        /// </summary>
        public bool IsFactory { get; set; }
        /// <summary>
        /// 经营工厂
        /// </summary>
        public Guid? Base_FactoryId { get; set; }
        /// <summary>
        /// 经营工厂
        /// </summary>
        public virtual Base_Factory Base_Factory { get; set; }
        /// <summary>
        /// 经营店铺
        /// </summary>
        public string StoreIds { get; set; }


        #region 贷款金额

        /// <summary>
        /// 申请贷款金额
        /// </summary>
        public decimal LoanAmount { get; set; }
        /// <summary>
        /// 实际发放金额
        /// </summary>
        public decimal GotLoanAmount { get; set; }

        #endregion

        #region 个人信息
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IDNo { get; set; }
        /// <summary>
        /// 身份证正反面
        /// </summary>
        public string IDImg { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string PersonalPhone { get; set; }

        #endregion

        #region 房产信息
        /// <summary>
        /// 是否有房产
        /// </summary>
        public bool HasHouse { get; set; }
        /// <summary>
        /// 房产所在省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 房产所在城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 房产所在县区
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 房产所在小区
        /// </summary>
        public string  Community { get; set; }
        /// <summary>
        /// 房产面积
        /// </summary>
        public string Area { get; set; }

        //以下为非必须资料

        /// <summary>
        /// 房产户主，与房产证上户主姓名一致
        /// </summary>
        public string Landlord { get; set; }
        /// <summary>
        /// 是否分期付款
        /// </summary>
        public bool IsInstallment { get; set; }
        /// <summary>
        /// 贷款银行标识（Code）,非必填项，根据是否分期来决定。
        /// </summary>
        public string FromBank { get; set; }

        #endregion

        #region 银行审批
        /// <summary>
        /// 审批银行
        /// </summary>
        public string BankCode { get; set; }
        /// <summary>
        /// 信贷经理
        /// </summary>
        public string BankManager { get; set; }
        /// <summary>
        /// 客户经理
        /// </summary>
        public string BankClerk { get; set; }

        #endregion


        /// <summary>
        /// 订单状态
        /// </summary>
        public G_OrderStatusEnum Status { get; set; }

        /// <summary>
        /// 金融客服工号
        /// </summary>
        public string GoJiajuClerkCode { get; set; }
        /// <summary>
        /// 金融经理工号
        /// </summary>
        public string GoJiajuManagerCode { get; set; }
    }
}
