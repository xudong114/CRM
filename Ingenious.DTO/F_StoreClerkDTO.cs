using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    /// <summary>
    /// 店铺和店员
    /// </summary>
    public class F_StoreClerkDTO : F_ModelRoot
    {
        /// <summary>
        /// 店铺Id
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// 店员Id
        /// </summary>
        public string UserCode { get; set; }
    }
}
