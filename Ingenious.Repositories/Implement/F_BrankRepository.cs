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
    public class F_BrankRepository : EntityFrameworkRepository<F_Brank>, IF_BrankRepository
    {
        public F_BrankRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_Brank> GetAll(ISpecification<F_Brank> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_Branks.Where(spec.GetExpression());
        }

    }
}
