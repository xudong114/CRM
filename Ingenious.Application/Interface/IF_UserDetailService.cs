using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_UserDetailService : IApplication<F_UserDetailDTO>
    {
        F_UserDetailDTO GetUserDetailByUserId(Guid userId);

        F_UserDetailDTO GetUserDetailByCode(string code);

        List<F_UserDetailDTO> GetUserDetailByBankCode(string bankCode);
    }
}
