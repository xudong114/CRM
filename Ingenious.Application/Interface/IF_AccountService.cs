using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_AccountService : IApplication<F_AccountDTO>
    {
        F_AccountDTO GetAccount(Guid userId);
        F_AccountDTO SetAlipay(F_AccountDTO account);

        F_AccountDTO ResetBalance(F_AccountDTO account);
    }
}
