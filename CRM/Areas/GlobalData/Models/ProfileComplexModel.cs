using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Areas.GlobalData.Models
{
    public class ProfileComplexModel
    {
        /// <summary>
        /// 个人信息
        /// </summary>
        public Base_ProfileDTO Profile { get; set; }
        /// <summary>
        /// 车辆信息
        /// </summary>
        public Base_CarDTO Car { get; set; }
        /// <summary>
        /// 车辆信息
        /// </summary>
        public Base_CarDTOList CarList { get; set; }
        /// <summary>
        /// 工厂信息
        /// </summary>
        public Base_FactoryDTO Factory { get; set; }
        /// <summary>
        /// 工厂信息
        /// </summary>
        public Base_FactoryDTOList FactoryList { get; set; }
        /// <summary>
        /// 店铺信息
        /// </summary>
        public Base_StoreDTO Store { get; set; }
        /// <summary>
        /// 店铺信息
        /// </summary>
        public List<Base_StoreDTO> StoreList { get; set; }
    }
}