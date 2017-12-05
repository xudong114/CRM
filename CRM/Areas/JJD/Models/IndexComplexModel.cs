using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Areas.JJD.Models
{
    public class IndexComplexModel
    {
        /// <summary>
        /// 首页广告位
        /// </summary>
        public List<G_ADDTO> ADs { get; set; }
        /// <summary>
        /// 产品列表
        /// </summary>
        public List<G_LoanProductDTO> Products { get; set; }

        /// <summary>
        /// 最新申请
        /// </summary>
        public G_OrderDTOListWithPagination LatestOrders { get; set; }

    }
}