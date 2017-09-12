
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_CreditCardHouseRepository : IRepository<F_CreditCardHouse>
    {
        IQueryable<F_CreditCardHouse> GetAll(ISpecification<F_CreditCardHouse> spec);
    }
}
