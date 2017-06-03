using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IAccountService : IApplication<AccountDTO>
    {
        AccountDTOList GetAll();
        DTO.AccountDTO GetByDepartmentId(Guid id);
    }
}
