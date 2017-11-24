using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Areas.JJD.Models
{
    /// <summary>
    /// 订单查询条件
    /// </summary>
    public class OrderFilterModel
    {
        public OrderFilterModel()
        {
            this.Status = G_OrderBusinessStatusEnum.Other;
            this.Keyword = string.Empty;
            this.ProductCode = string.Empty;
            this.Month = string.Empty;
            this.BankClerkCode = string.Empty;
            this.IsFilter = false;
        }
        /// <summary>
        /// 是否过滤
        /// </summary>
        public bool IsFilter { get; set; }
        /// <summary>
        /// 订单关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 订单进度
        /// </summary>
        public G_OrderBusinessStatusEnum Status { get; set; }
        /// <summary>
        /// 贷款类型
        /// </summary>
        public string ProductCode { get; set; }
        /// <summary>
        /// 月份(yyyy-MM)
        /// </summary>
        public string Month { get; set; }
        /// <summary>
        /// 信贷经理
        /// </summary>
        public string BankClerkCode { get; set; }

        /// <summary>
        /// 产品列表
        /// </summary>
        public List<G_LoanProductDTO> Products { get; set; }
    }
}