using Ingenious.Domain.Models;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class ClientContactRepository : EntityFrameworkRepository<ClientContact>, IClientContactRepository
    {
        public ClientContactRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<ClientContact> GetContactsByClientId(Guid clientId)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.ClientContacts.Where(item => item.ClientId.Equals(clientId));
        }


    }
}
