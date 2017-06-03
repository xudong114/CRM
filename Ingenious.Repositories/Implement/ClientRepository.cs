using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class ClientRepository : EntityFrameworkRepository<Client>, IClientRepository
    {
        public ClientRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Client> GetAll(ISpecification<Client> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Clients.Where(spec.GetExpression());
        }

    }
}
