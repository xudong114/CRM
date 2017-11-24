using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_LoanProductService : IApplication<G_LoanProductDTO>
    {
        List<G_LoanProductDTO> GetAll(string code = "", bool? isActive = true);
    }
}
