using Ingenious.DTO;
using Ingenious.Infrastructure;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_BankService : IApplication<F_BankDTO>
    {
        F_BankDTO GetBankByUserId(Guid id);
        F_BankDTO GetBank(string bankCode);
        List<F_BankDTO> GetBanks(string bankCode = "", bool? isAdmin = null, string sort = "order");
        string AssignOrderToClerk(string bankCode);
    }
}
