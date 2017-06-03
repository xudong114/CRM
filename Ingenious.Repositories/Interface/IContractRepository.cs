using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Interface
{
    public interface IClientRepository : IRepository<Client>
    {
        IQueryable<Client> GetAll(ISpecification<Client> spec);
    }
}
