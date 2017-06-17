using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IContractService : IApplication<ContractDTO>
    {
        ContractDTOList GetAll(string keywords = "", Guid? clientId = null, Guid? userId = null, Guid? departmentId = null);
    }
}
