
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_BankOptionRepository : IRepository<F_BankOption>
    {
        IQueryable<F_BankOption> GetAll(ISpecification<F_BankOption> spec);
        F_BankOption GetBankOptionByBankId(Guid bankId);
    }
}
