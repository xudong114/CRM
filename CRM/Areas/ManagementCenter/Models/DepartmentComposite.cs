using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Areas.ManagementCenter.Models
{
    public class DepartmentComposite
    {
        public DepartmentDTO DepartmentDTO { get; set; }
        public AccountDTO AccountDTO { get; set; }
        public BillDTOList BillDTOList { get; set; }
        public RechargeDTOList RechargeDTOList { get; set; }
        public PriceStrategyDTO PriceStrategyDTO { get; set; }
    }
}