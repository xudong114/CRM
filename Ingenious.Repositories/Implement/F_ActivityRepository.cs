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
    public class F_ActivityRepository : EntityFrameworkRepository<F_Activity>, IF_ActivityRepository
    {
        public F_ActivityRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_Activity> GetAll(ISpecification<F_Activity> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_Activities.Where(spec.GetExpression());
        }
    }
}
