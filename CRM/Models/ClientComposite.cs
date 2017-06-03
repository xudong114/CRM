using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class ClientComposite
    {
        public ClientDTO Client { get; set; }
        public ClientDTOList ClientDTOList { get; set; }
        /// <summary>
        /// 客户活动记录
        /// </summary>
        public ActivityDTOList ActivityDTOList { get; set; }


    }
}