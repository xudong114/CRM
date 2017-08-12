using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IF_ActivityService : IApplication<F_ActivityDTO>
    {
        List<F_ActivityDTO> GetAll(Guid? referenceId, bool? isRead, bool? isGlobal);
        int GetMessageCount(Guid referenceId);
    }
}
