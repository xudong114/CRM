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
    public interface IG_BankService : IApplication<G_BankDTO>
    {
        /// <summary>
        /// 根据银行编码获取银行用户
        /// </summary>
        /// <param name="bankCode">银行编码</param>
        /// <returns></returns>
        List<G_UserDetailDTO> GetUserByBank(string bankCode);
        G_BankDTO GetBankByUserId(Guid id);
        G_BankDTO GetBank(string bankCode);
        List<G_BankDTO> GetBanks(string bankCode = "", bool? isAdmin = null, string sort = "order");
        string AssignOrderToClerk(string bankCode);
    }
}
