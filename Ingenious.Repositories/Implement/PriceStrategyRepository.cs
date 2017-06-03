using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System.Linq;

namespace Ingenious.Repositories.Implement
{
    public class PriceStrategyRepository : EntityFrameworkRepository<PriceStrategy>, IPriceStrategyRepository
    {
        public PriceStrategyRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<PriceStrategy> GetAll(ISpecification<PriceStrategy> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.PriceStrategies.Where(spec.GetExpression());
        }

    }
}
