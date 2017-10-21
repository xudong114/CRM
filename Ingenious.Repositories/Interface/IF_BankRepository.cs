
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_BankRepository : IRepository<F_Bank>
    {
        IQueryable<F_Bank> GetAll(ISpecification<F_Bank> spec,string sort = "order");
        F_Bank GetBankByUserId(Guid id);
        IQueryable<Infrastructure.KeyValue<string, int>> AssignOrderToClerk(string bankCode);
    }
}
