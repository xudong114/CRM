using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_UserService : IApplication<F_UserDTO>
    {
        F_UserDTO Login(F_UserDTO user);
        F_UserDTO GetUserByUserName(string userName);
        F_UserDTOList GetUsers(bool? isActive = true, string keywords = "", F_UserTypeEnum? userType = null, string order = "createddate_desc");
        F_UserDTOList GetBankUser(string bankcode = "", bool? isactive = null);
    }
}
