using Ingenious.Domain.Models;
using Ingenious.Domain.Specifications;
using Ingenious.Repositories.EntityFramework;
using Ingenious.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ingenious.Repositories.Implement
{
    public class RechargeRepository : EntityFrameworkRepository<Recharge>, IRechargeRepository
    {
        public RechargeRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Recharge> GetAll(ISpecification<Recharge> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Recharges.Where(spec.GetExpression());
        }

        public IQueryable<Recharge> GetByAccountId(Guid id)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Recharges.Where(item => item.AccountId == id);
        }

    }
}
