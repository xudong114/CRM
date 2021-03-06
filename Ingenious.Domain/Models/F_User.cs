﻿using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Domain.Models
{
    /// <summary>
    /// 金融用户账号
    /// </summary>
    public class F_User : AggregateRoot
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public F_UserTypeEnum UserType { get; set; }
        /// <summary>
        /// 所属站点
        /// </summary>
        public Guid WebsiteId { get; set; }
    }
}
