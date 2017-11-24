using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Areas.JJD.Models
{
    public class OrderComplexModel
    {
        /// <summary>
        /// 工厂
        /// </summary>
        public Base_FactoryDTO Factory { get; set; }
        /// <summary>
        /// 经销商
        /// </summary>
        public List<Base_StoreDTO> Stores { get; set; }
        /// <summary>
        /// 订单
        /// </summary>
        public G_OrderDTO Order { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public Guid OrderId { get; set; }
    }
}