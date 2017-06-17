using Ingenious.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Application.Interface
{
    public interface IClientContactService : IApplication<ClientContactDTO>
    {
        ClientContactDTOList GetContactsByClientId(Guid clientId);
    }
}
