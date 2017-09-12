using Ingenious.Domain.Specifications;
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
    public interface IF_BankOptionService : IApplication<F_BankOptionDTO>
    {
        List<F_BankOptionDTO> GetAll();
        F_BankOptionDTO GetBankOptionByBankId(Guid bankId);
    }
}
