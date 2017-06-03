using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.DTO
{
    public class ActivityCategoryDTO : ModelRoot
    {
        public string Name { get; set; }
        public string Desc { get; set; }
    }

    public class ActivityCategoryDTOList : List<ActivityCategoryDTO>
    { }

}
