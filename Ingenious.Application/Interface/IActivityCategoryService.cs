using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IActivityCategoryService : IApplication<ActivityCategoryDTO>
    {
        ActivityCategoryDTOList GetAll(string sort = "created_desc");
    }
}
