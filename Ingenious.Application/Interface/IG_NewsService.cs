using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_NewsService : IApplication<G_NewsDTO>
    {
        List<G_NewsDTO> GetAll(string code="",string title="");
    }
}
