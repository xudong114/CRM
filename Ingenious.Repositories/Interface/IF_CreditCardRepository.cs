
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_CreditCardRepository : IRepository<F_CreditCard>
    {
        IQueryable<F_CreditCard> GetAll(ISpecification<F_CreditCard> spec);
    }
}
