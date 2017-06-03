using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IRechargeService : IApplication<RechargeDTO>
    {
        RechargeDTOList GetAll();
        RechargeDTOList GetByAccountId(Guid id);

    }
}
