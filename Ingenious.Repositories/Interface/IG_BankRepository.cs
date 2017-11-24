
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IG_BankRepository : IRepository<G_Bank>
    {
        IQueryable<G_Bank> GetAll(ISpecification<G_Bank> spec,string sort = "order");
        G_Bank GetBankByUserId(Guid id);
        IQueryable<Infrastructure.KeyValue<string, int>> AssignOrderToClerk(string bankCode);
        int GetMaxCode();
    }
}
