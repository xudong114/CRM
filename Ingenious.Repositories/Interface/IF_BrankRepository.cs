
using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IF_BrankRepository : IRepository<F_Brank>
    {
        IQueryable<F_Brank> GetAll(ISpecification<F_Brank> spec);
    }
}
