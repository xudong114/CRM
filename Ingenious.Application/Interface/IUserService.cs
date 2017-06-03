using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IUserService : IApplication<UserDTO>
    {
        UserDTO Login(UserDTO user);
        UserDTOList GetUsers(Guid? department, UserStatusEnum status ,string keywords = "",string order = "createddate_desc");
    }
}
