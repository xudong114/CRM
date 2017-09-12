
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_CreditCardFileRepository : IRepository<F_CreditCardFile>
    {
        IQueryable<F_CreditCardFile> GetAll(ISpecification<F_CreditCardFile> spec);
    }
}
