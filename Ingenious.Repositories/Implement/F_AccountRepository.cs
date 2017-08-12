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
    public class F_AccountRepository : EntityFrameworkRepository<F_Account>, IF_AccountRepository
    {
        public F_AccountRepository(IRepositoryContext context) 
            : base(context) 
        {
            
        }

        public IQueryable<F_Account> GetAll(ISpecification<F_Account> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_Accounts.Where(spec.GetExpression());
        }

        public F_Account GetAccount(Guid userId)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.F_Accounts.Where(item => item.UserId.Equals(userId)).FirstOrDefault();
        }
    }
}
