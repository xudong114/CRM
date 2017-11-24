using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_ADService : IApplication<G_ADDTO>
    {
        List<G_ADDTO> GetAll(string code);
        List<G_ADDTO> GetAll(string[] codes);
    }
}
