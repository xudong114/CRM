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
    public class F_ADRepository : EntityFrameworkRepository<F_AD>, IF_ADRepository
    {
        public F_ADRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_AD> GetAll(ISpecification<F_AD> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_ADs.Where(spec.GetExpression());
        }

    }
}
