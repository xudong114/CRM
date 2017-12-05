using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IBase_AccountService : IApplication<Base_AccountDTO>
    {
        /// <summary>
        /// 根据身份证号码获取账户信息
        /// </summary>
        /// <param name="idNo">身份证号码</param>
        /// <returns></returns>
        Base_AccountDTOList GetAccountByIDNo(string idNo);
    }
}
