using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_ActivityService : IApplication<G_ActivityDTO>
    {
        G_ActivityDTOListWithPagination GetAll(int pageIndex, int pageSize, Guid? referenceId, bool? isRead, bool? isGlobal, string sort = "createddate_desc");
        int GetMessageCount(Guid referenceId);
    }
}
