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
    public class F_UserDetailRepository : EntityFrameworkRepository<F_UserDetail>, IF_UserDetailRepository
    {
        public F_UserDetailRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_UserDetail> GetAll(ISpecification<F_UserDetail> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_UserDetails.Where(spec.GetExpression());
        }

    }
}
