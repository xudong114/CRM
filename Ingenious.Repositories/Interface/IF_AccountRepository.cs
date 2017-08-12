
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_AccountRepository : IRepository<F_Account>
    {
        IQueryable<F_Account> GetAll(ISpecification<F_Account> spec);
        F_Account GetAccount(Guid userId);
    }
}
