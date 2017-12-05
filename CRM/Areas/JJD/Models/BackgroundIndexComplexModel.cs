using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Areas.JJD.Models
{
    /// <summary>
    /// 后台首页
    /// </summary>
    public class BackgroundIndexComplexModel
    {
        /// <summary>
        /// 未提交数量
        /// </summary>
        public int TempCount { get; set; }
        /// <summary>
        /// 正在申请数量
        /// </summary>
        public int InProcessCount { get; set; }
        /// <summary>
        /// 已通过数量
        /// </summary>
        public int SuccessedCount { get; set; }
        /// <summary>
        /// 已失效
        /// </summary>
        public int PassedCount { get; set; }

        /// <summary>
        /// 消息数量
        /// </summary>
        public int MessageCount { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public G_UserDTO User {get;set;}
    }
}