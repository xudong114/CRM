using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class F_UserDTO : F_ModelRoot
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public F_UserType UserType { get; set; }
    }

    public class F_UserDTOList:List<F_UserDTO>
    { }
}
