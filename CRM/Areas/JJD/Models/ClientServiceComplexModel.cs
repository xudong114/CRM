using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Areas.JJD.Models
{
    /// <summary>
    /// 金融客服复合体
    /// </summary>
    public class ClientServiceComplexModel
    {
        /// <summary>
        /// 待受理数量
        /// </summary>
        public int PreProcessCount { get; set; }
        /// <summary>
        /// 待签约数量
        /// </summary>
        public int PreSignCount { get; set; }
        /// <summary>
        /// 待放款数量
        /// </summary>
        public int PreSuccessedCount { get; set; }
        /// <summary>
        /// 已放款数量
        /// </summary>
        public int SuccessedCount { get; set; }
        /// <summary>
        /// 已失效数量
        /// </summary>
        public int PassedCount { get; set; }
        /// <summary>
        /// 消息数量
        /// </summary>
        public int MessageCount { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public G_UserDTO User { get; set; }
    }
}