
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_CreditCardStoreRepository : IRepository<F_CreditCardStore>
    {
        IQueryable<F_CreditCardStore> GetAll(ISpecification<F_CreditCardStore> spec);
    }
}
