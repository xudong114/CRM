using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Ingenious.Repositories.Implement
{
    public class PriceStrategyDetailRepository : EntityFrameworkRepository<PriceStrategyDetail>, IPriceStrategyDetailRepository
    {
        public PriceStrategyDetailRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<PriceStrategyDetail> GetAll(ISpecification<PriceStrategyDetail> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.PriceStrategyDetails.Where(spec.GetExpression());
        }

        public List<PriceStrategyDetail> Create(List<PriceStrategyDetail> list)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            list = context.PriceStrategyDetails.AddRange(list).ToList();
            context.SaveChanges();
            return list;
        }
    }
}
