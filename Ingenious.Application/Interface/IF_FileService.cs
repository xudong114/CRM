using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_FileService : IApplication<F_FileDTO>
    {
        F_FileDTOList GetFilesByReferenceId(Guid referenceId);
        F_FileDTOList GetFilesByReferenceId(Guid referenceId, string code = "");
    }
}
