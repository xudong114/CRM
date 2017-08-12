using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_WithdrawDepositRecordService : IApplication<F_WithdrawDepositRecordDTO>
    {
        F_WithdrawDepositRecordDTOList GetAll(string clerkCode = "");
    }
}
