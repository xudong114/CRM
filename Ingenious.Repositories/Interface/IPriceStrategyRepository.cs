using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using System.Collections.Generic;
using System.Linq;

namespace Ingenious.Repositories.Interface
{
    public interface IPriceStrategyDetailRepository : IRepository<PriceStrategyDetail>
    {
        IQueryable<PriceStrategyDetail> GetAll(ISpecification<PriceStrategyDetail> spec);

        List<PriceStrategyDetail> Create(List<PriceStrategyDetail> list);
    }
}
