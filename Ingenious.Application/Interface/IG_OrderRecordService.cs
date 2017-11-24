using Ingenious.Domain.DataSource;
using Ingenious.DTO;
using Ingenious.Infrastructure.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IG_OrderRecordService : IApplication<G_OrderRecordDTO>
    {
       List<G_LoanProductDTO> GetAll();
    }
}
