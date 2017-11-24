using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_UserDetailService : IApplication<G_UserDetailDTO>
    {
        G_UserDetailDTO GetUserDetailByUserId(Guid userId);

        G_UserDetailDTO GetUserDetailByCode(string code);

        List<G_UserDetailDTO> GetUserDetailByBankCode(string bankCode);
    }
}
