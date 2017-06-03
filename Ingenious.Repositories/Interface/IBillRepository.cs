using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Interface
{
    public interface IBillRepository : IRepository<Bill>
    {
        IQueryable<Bill> GetByAccountId(Guid id);
        IQueryable<Bill> GetAll(ISpecification<Bill> spec);
    }
}
