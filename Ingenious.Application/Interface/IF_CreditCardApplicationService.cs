using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_CreditCardApplicationService : IApplication<F_CreditCardApplicationDTO>
    {
        List<F_CreditCardApplicationDTO> GetAll();
    }
}
