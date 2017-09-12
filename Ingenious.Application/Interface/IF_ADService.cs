using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_ADService : IApplication<F_ADDTO>
    {
        List<F_ADDTO> GetAll(string code);
        List<F_ADDTO> GetAll(string[] codes);
    }
}
