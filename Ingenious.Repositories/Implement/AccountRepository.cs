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
    public class AccountRepository : EntityFrameworkRepository<Account>, IAccountRepository
    {
        public AccountRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public IQueryable<Account> GetAll(ISpecification<Account> spec)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Accounts.Where(spec.GetExpression());
        }

        public Account GetByDepartmentId(Guid id)
        {
            var context = this.EFContext.Context as IngeniousDbContext;
            return context.Accounts.Where(item => item.DepartmentId == id).FirstOrDefault();
        }

    }
}
