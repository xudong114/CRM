
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_CreditCardApplicationRepository : IRepository<F_CreditCardApplication>
    {
        IQueryable<F_CreditCardApplication> GetAll(ISpecification<F_CreditCardApplication> spec);
    }
}
