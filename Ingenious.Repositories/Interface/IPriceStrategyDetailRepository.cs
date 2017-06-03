using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IPriceStrategyRepository : IRepository<PriceStrategy>
    {
        IQueryable<PriceStrategy> GetAll(ISpecification<PriceStrategy> spec);
    }
}
