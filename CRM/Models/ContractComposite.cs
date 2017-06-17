using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Models
{
    public class ContractComposite
    {
        public DepartmentDTO DepartmentDTO { get; set; }
        public AccountDTO AccountDTO { get; set; }
        public ContractDTO ContractDTO { get; set; }
        public ClientDTOList ClientDTOList { get; set; }
        public ProductDTOList ProductDTOList { get; set; }
        public UserDTOList UserDTOList { get; set; }
        public PriceStrategyDTOList PriceStrategyDTOList { get; set; }
        public DepartmentDTOList DepartmentDTOList { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public UserDTO User { get; set; }
    }
}