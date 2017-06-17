using Ingenious.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Interface
{
    public interface IClientContactRepository : IRepository<ClientContact>
    {
        IQueryable<ClientContact> GetContactsByClientId(Guid clientId);

    }
}
