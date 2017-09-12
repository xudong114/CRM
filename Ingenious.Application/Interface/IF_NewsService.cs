using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_NewsService : IApplication<F_NewsDTO>
    {
        List<F_NewsDTO> GetAll(string code="",string title="");
    }
}
