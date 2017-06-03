using Ingenious.DTO;
using System;

namespace Ingenious.Application.Interface
{
    public interface IActivityService : IApplication<ActivityDTO>
    {
        ActivityDTOList GetAll(Guid? referenceId, Guid? UserId, string sort = "created_desc");
    }
}
