using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_FileService : IApplication<G_FileDTO>
    {
        G_FileDTOList GetFilesByReferenceId(Guid referenceId);
        G_FileDTOList GetFilesByReferenceId(Guid referenceId, string code = "");
    }
}
