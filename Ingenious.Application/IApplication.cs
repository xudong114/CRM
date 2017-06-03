using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application
{
    public interface IApplication<DTO>
    {
        DTO GetByKey(Guid id);

        DTO Create(DTO dto);

        List<DTO> Update(List<DTO> dtoList);

        void Delete(List<DTO> dtoList);
    }
}
