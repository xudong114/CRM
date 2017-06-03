using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Interface
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetByDepartmentId(Guid id);
        IQueryable<Account> GetAll(ISpecification<Account> spec);
    }
}
