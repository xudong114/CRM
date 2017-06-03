using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRM.Areas.ManagementCenter.Models
{
    public class UserDTOComposite
    {
        public UserDTO UserDTO { get; set; }
        public DepartmentDTOList DepartmentDTOList { get; set; }
    }
}