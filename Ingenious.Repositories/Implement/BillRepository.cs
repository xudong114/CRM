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
    public class BillRepository : EntityFrameworkRepository<Bill>, IBillRepository
    {
        public BillRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Bill> GetAll(ISpecification<Bill> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Bills.Where(spec.GetExpression());
        }

        public IQueryable<Bill> GetByAccountId(Guid id)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Bills.Where(item => item.AccountId == id);
        }

    }
}
