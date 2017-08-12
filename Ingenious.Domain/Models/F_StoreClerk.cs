using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 店铺和店员
    /// </summary>
    public class F_StoreClerk : AggregateRoot
    {
        /// <summary>
        /// 店铺Id
        /// </summary>
        public string StoreCode { get; set; }
        /// <summary>
        /// 店员Id
        /// </summary>
        public string  UserCode { get; set; }
    }
}
